using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rich.Common.Base.RichReturnModel;
using Rich.Order.Application.UserAppService;
using Rich.Order.Domain.User;

namespace Rich.Order.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private SignInManager<RichOrderUser> _signInManager;
        private UserManager<RichOrderUser> _userManager;

        public AccountController(SignInManager<RichOrderUser> signInManager, UserManager<RichOrderUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //[HttpPost("LoginOut")]
        //public async Task<IApiReturnModel<object>> LoginOut()
        //{
        //    await _signInManager.SignOutAsync();

        //}
        [HttpPost("Login")]
        //[Route("Login")]
        public async Task<IApiReturnModel<object>> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new RichApiReturnBoolModel(false, "参数校验失败");
            }
            //根据用户名匹配
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return new RichApiReturnBoolModel(false, "用户不存在");
            }
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            //检查密码格式
            var sysbom=await _userManager.CheckPasswordAsync(user, model.Password);
            if (!sysbom)
            {
                return new RichApiReturnBoolModel(false, "密码格式不正确");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false,false);

            if (result.Succeeded)
            {
                return new RichApiReturnBoolModel(true, "登录成功");
            }
            else
            {
                return new RichApiReturnBoolModel(50000,false, "登录失败");
            }
        }
        [HttpPost("Register")]
        public async Task<IApiReturnModel<object>> Register([FromBody]RichOrderUser user)
        {
          
            var passHash=_userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
            user.PasswordHash = passHash;
            var result = await _userManager.CreateAsync(user);
          


            if (result.Succeeded)
            {

                return new RichApiReturnBoolModel(true, "新增用户成功");
            }
            else
            {
                return new RichApiReturnBoolModel(false, string.Join(',', result.Errors));
            }
        }
    }
}