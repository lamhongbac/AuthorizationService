using AuthorizationService.Data;
using AuthorizationService.Service;
using AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Models;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AuthenticationService _authenticationService;
        
        public AccountController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Login(LoginModel model)
        {
            IActionResult response = Unauthorized();
            UserInfo? user = _authenticationService.AuthenticateUser(model);
            LoginInfo loginInfo = new LoginInfo();
            if (user != null)
            {
                JwtData jwtData = _authenticationService.GenerateJSONWebToken(user);
                loginInfo.JwtData = jwtData;
                response = Ok(user);
            }

            return response;
        }
        public IActionResult Logout(LogOutModel model) 
        { 
            var logutResult= _authenticationService.Logout(model);
            return Ok(logutResult);
        }
        public IActionResult RenewToken(JwtData model)
        {
            var renewTokenResult = _authenticationService.RenewToken(model);
            return Ok(renewTokenResult);
        }
    }
}
