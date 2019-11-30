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


        public AccountController(SignInManager<RichOrderUser> signInManager, UserManager<RichOrderUser> userManager, RoleManager<RichOrderRole> roleManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<IApiReturnModel<LoginViewOutput>> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new RichApiReturnModel<LoginViewOutput>(RichConst.FailCode,false, "参数校验失败");
            }
            //根据用户名匹配
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return new RichApiReturnModel<LoginViewOutput>(RichConst.FailCode, false, "用户不存在");
            }
            //检查密码格式
            var sysbom = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!sysbom)
            {
                return new RichApiReturnModel<LoginViewOutput>(RichConst.FailCode, false, "密码格式不正确");
            }

            //密码hash加密
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false,false);
            if (result.Succeeded)
            {
                LoginViewOutput output=new LoginViewOutput();
                output.Code = RichConst.SuccessCode.ToString();

               var roles=await _userManager.GetRolesAsync(user);
               output.Data=new VueLoginData();
               output.Data.Roles = roles.ToList();
               foreach (var role in roles)
               {
                   var roleTask = _roleManager.FindByNameAsync(role).ConfigureAwait(false);
                   var roleEnity=(RichOrderRole)roleTask.GetAwaiter().GetResult();
                   output.Data.Name = roleEnity.ShowName;
                   output.Data.Avatar = roleEnity.Avatar;
                   output.Data.Introduction = roleEnity.Introduction;
               }

                return new RichApiReturnModel<LoginViewOutput>(RichConst.SuccessCode, true, output, "登录成功");
            }
            else
            {
                return new RichApiReturnModel<LoginViewOutput>(RichConst.FailCode, false, "登录失败");
            }
        }
        [HttpPost("AddRole")]
        //[Authorize]
        public async Task<IApiReturnModel<object>> AddRole(string roleName,string showName,string introduction,string avatar)
        {
            RichOrderRole role=new RichOrderRole();
            role.Name = roleName;
            role.Avatar = avatar;
            role.ShowName = showName;
            role.Introduction = introduction;

            var result=await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return new RichApiReturnBoolModel(true, "添加成功");
            }
            else
            {
                return new RichApiReturnBoolModel(50000, false, "添加失败");
            }
        }
        [HttpPost("LoginOut")]
        public async void LoginOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception e)
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;
                // "User: {User} TraceId:{TraceId} Msg:{Msg} CreateTime:{CreateTime}";
                _logger.Error("",new object[]{  username, _httpContextAccessor.HttpContext.TraceIdentifier,e.Message,DateTime.Now });
            }
          
        }
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
            var passHash=_userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
            user.PasswordHash = passHash;
            //创建用户
            var result = await _userManager.CreateAsync(user);
            //创建成功
            if (result.Succeeded)
            {
                var alreadExistUser=await _userManager.FindByNameAsync(user.UserName);
  
                viewRegister.roleId.ForEach(p =>
                {
                    var rolesTask = _roleManager.FindByIdAsync(p).ConfigureAwait(false);
                    var roleentity = rolesTask.GetAwaiter().GetResult();
                    var re= _userManager.AddToRoleAsync(alreadExistUser, roleentity.Name).ConfigureAwait(false).GetAwaiter().GetResult();
                });


                List<Claim> claims=new List<Claim>();

                viewRegister.roleId.ForEach(p =>
                {
                    var claim=new Claim(ClaimTypes.Role, p);
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
    }
}