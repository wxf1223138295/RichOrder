using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Rich.Common.Base.AccessorDependencyInjection;
using Rich.Common.Base.Common;
using Rich.Common.Base.RichReturnModel;
using Rich.Order.Application.UserAppService;
using Rich.Order.Domain.User;
using Serilog;

namespace Rich.Order.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SignInManager<RichOrderUser> _signInManager;
        private UserManager<RichOrderUser> _userManager;
        private RoleManager<RichOrderRole> _roleManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;
        private ILogger _logger;
        private IRichUserAppService _richUserAppService;


        public AccountController(SignInManager<RichOrderUser> signInManager, UserManager<RichOrderUser> userManager, RoleManager<RichOrderRole> roleManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger logger, IRichUserAppService richUserAppService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
            _richUserAppService = richUserAppService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IApiReturnModel<LoginOutView>> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new RichApiReturnModel<LoginOutView>(RichConst.FailCode, false, "参数校验失败");
            }
            //根据用户名匹配
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return new RichApiReturnModel<LoginOutView>(RichConst.FailCode, false, "用户不存在");
            }
            //检查密码格式
            var sysbom = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!sysbom)
            {
                return new RichApiReturnModel<LoginOutView>(RichConst.FailCode, false, "密码格式不正确");
            }
            //密码hash加密
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                //目前返回单角色
                var roles = await _userManager.GetRolesAsync(user);
                if (roles == null || roles.Count == 0)
                {
                    return new RichApiReturnModel<LoginOutView>(RichConst.FailCode, false, "登录失败-未获取到角色");
                }
                var roleentiy = await _roleManager.FindByNameAsync(roles.FirstOrDefault());
                LoginOutView outView = new LoginOutView();
                outView.Code = RichConst.SuccessCode.ToString();
                outView.Token = roleentiy.RoleTokenName;

                return new RichApiReturnModel<LoginOutView>(RichConst.SuccessCode, true, outView, "登录成功");
            }
            else
            {
                return new RichApiReturnModel<LoginOutView>(RichConst.FailCode, false, "登录失败");
            }
        }
        /// <summary>
        /// 目前roletoken对应一个角色 单角色
        /// </summary>
        /// <param name="roleTokenName"></param>
        /// <returns></returns>
        [HttpGet("UserInfo")]
        public async Task<IApiReturnModel<LoginViewOutput>> UserInfo(string token)
        {
            var roleEntity = await _roleManager.Roles.Where(p => p.RoleTokenName == token).ToListAsync();
            if (roleEntity == null || roleEntity.Count == 0)
            {
                return new RichApiReturnModel<LoginViewOutput>(RichConst.FailCode, false, "获取角色信息失败");
            }
            LoginViewOutput output = new LoginViewOutput();
            output.Code = RichConst.SuccessCode.ToString();

            output.Data = new VueLoginData();
            List<string> list = new List<string>();
            list.Add(roleEntity.FirstOrDefault()?.Name);
            output.Data.Roles = list;
            foreach (var role in roleEntity)
            {
                var roleTask = _roleManager.FindByNameAsync(role.Name).ConfigureAwait(false);
                var roleEnity = (RichOrderRole)roleTask.GetAwaiter().GetResult();
                output.Data.Name = roleEnity.ShowName;
                output.Data.Avatar = roleEnity.Avatar;
                output.Data.Introduction = roleEnity.Introduction;
                var bizreturn = _richUserAppService.GetRouterByRoleId(roleEnity.Id).ConfigureAwait(false).GetAwaiter()
                    .GetResult();
                if (bizreturn.success)
                {
                    output.Data.RoleRouters = bizreturn.data;
                }
                else
                {
                    // // "User: {User} TraceId:{TraceId} Msg:{Msg} CreateTime:{CreateTime}";
                    _logger.Error("",new object[]{"1","1",bizreturn.exceptionMsg,DateTime.Now});
                }
            }

            return new RichApiReturnModel<LoginViewOutput>(RichConst.SuccessCode, true, output, "获取用户信息成功");




        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="showName"></param>
        /// <param name="introduction"></param>
        /// <param name="avatar"></param>
        /// <param name="roletokenname"></param>
        /// <returns></returns>
        [HttpPost("AddRole")]
        //[Authorize]
        public async Task<IApiReturnModel<object>> AddRole(string roleName, string showName, string introduction, string avatar,string roletokenname)
        {
            RichOrderRole role = new RichOrderRole();
            role.Name = roleName;
            role.Avatar = avatar;
            role.ShowName = showName;
            role.Introduction = introduction;
            role.RoleTokenName = roletokenname;

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new RichApiReturnBoolModel(true, "添加成功");
            }
            else
            {
                return new RichApiReturnBoolModel(RichConst.FailCode, false, "添加失败");
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost("LoginOut")]
        public async Task<IApiReturnModel<LoginOutViewModel>> LoginOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                LoginOutViewModel model = new LoginOutViewModel
                {
                    Code = RichConst.SuccessCode,
                    Data = "success"
                };
                return new RichApiReturnModel<LoginOutViewModel>(RichConst.SuccessCode,true,model,"退出成功");
            }
            catch (Exception e)
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;
                // "User: {User} TraceId:{TraceId} Msg:{Msg} CreateTime:{CreateTime}";
                _logger.Error("", new object[] { username, _httpContextAccessor.HttpContext.TraceIdentifier, e.Message, DateTime.Now });
                LoginOutViewModel model = new LoginOutViewModel
                {
                    Code = RichConst.FailCode,
                    Data = "fail"
                };
                return new RichApiReturnModel<LoginOutViewModel>(RichConst.FailCode, false, model, "退出失败");
            }

        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="viewRegister"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        // [Authorize]
        public async Task<IApiReturnModel<object>> Register([FromBody]RegisterViewModel viewRegister)
        {
            //
            if (!ModelState.IsValid)
            {
                return new RichApiReturnBoolModel(false, "参数校验失败");
            }
            //映射
            var user = _mapper.Map<RichOrderUser>(viewRegister);
            //密码加密
            var passHash = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
            user.PasswordHash = passHash;
            //创建用户
            var result = await _userManager.CreateAsync(user);
            //创建成功
            if (result.Succeeded)
            {
                var alreadExistUser = await _userManager.FindByNameAsync(user.UserName);

                viewRegister.roleId.ForEach(p =>
                {
                    var rolesTask = _roleManager.FindByIdAsync(p).ConfigureAwait(false);
                    var roleentity = rolesTask.GetAwaiter().GetResult();
                    var re = _userManager.AddToRoleAsync(alreadExistUser, roleentity.Name).ConfigureAwait(false).GetAwaiter().GetResult();
                });


                List<Claim> claims = new List<Claim>();

                viewRegister.roleId.ForEach(p =>
                {
                    var claim = new Claim(ClaimTypes.Role, p);
                    claims.Add(claim);
                });

                claims.Add(new Claim(ClaimTypes.Name, user.Id));

                var res1 = await _userManager.AddClaimsAsync(alreadExistUser, claims);

                return new RichApiReturnBoolModel(true, "新增用户成功");
            }
            else
            {
                return new RichApiReturnBoolModel(false, string.Join(',', result.Errors));
            }
        }

        [HttpGet("Roles")]
        public async Task<IApiReturnModel<IEnumerable<RoleListOutView>>> GetRichRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
            {
                return new RichApiReturnModel<IEnumerable<RoleListOutView>>(RichConst.SuccessCode,true,new List<RoleListOutView>(),"未获取到数据");
            }
            var returnList = _mapper.Map<IEnumerable<RoleListOutView>>(roles);
            returnList.ToList().ForEach(p =>
            {
                var roleid=_roleManager.Roles.Where(q => q.Name == p.Name).FirstOrDefault()?.Id;
                var rots= _richUserAppService.GetRouterByRoleId(roleid).ConfigureAwait(false).GetAwaiter().GetResult();
                if (rots.success)
                {
                    p.Routes = rots.data;
                }

            });


            return new RichApiReturnModel<IEnumerable<RoleListOutView>>(RichConst.SuccessCode, true, returnList
                , "获取角色信息成功");
        }

        [HttpGet("OrderRouter")]
        public async Task<IApiReturnModel<IEnumerable<PagePermissionViewModel>>> GetOrderRouter()
        {
            var returnResult = new RichApiReturnModel<IEnumerable<PagePermissionViewModel>>();
            var reuslt = await _richUserAppService.GetRouterByRoleId();
            if (reuslt.success)
            {
                returnResult.success = reuslt.success;
                returnResult.data = reuslt.data;
                returnResult.resultCode = RichConst.SuccessCode;
                returnResult.message = "获取成功";
            }
            else
            {
                returnResult.success = reuslt.success;
                returnResult.data = null;
                returnResult.resultCode = RichConst.FailCode;
                returnResult.message =reuslt.message;
                returnResult.exceptionMsg = reuslt.exceptionMsg;
            }
            return returnResult;
        }
    }
}