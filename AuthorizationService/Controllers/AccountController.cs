using AuthorizationService.Data;
using AuthorizationService.Service;
using AuthServices;
using Microsoft.AspNetCore.Mvc;
using SharedLib;
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
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            IActionResult response = Unauthorized();
            UserInfo user = await _authenticationService.AuthenticateUser(model);
            SharedLib.LoginInfo loginInfo = new SharedLib.LoginInfo();
            if (user != null)
            {
                JwtData jwtData = _authenticationService.GenerateJSONWebToken(user);
                loginInfo.JwtData = jwtData;
                response = Ok(user);
            }

            return response;
        }
        [Route("Logout")]
        [HttpPost]
        public IActionResult Logout(LogOutModel model)
        {
            var logutResult = _authenticationService.Logout(model);
            return Ok(logutResult);
        }
        [Route("RenewToken")]
        [HttpPost]
        public IActionResult RenewToken(JwtData model)
        {
            var renewTokenResult = _authenticationService.RenewToken(model);
            return Ok(renewTokenResult);
        }
    }
}
