using AuthorizationService.Data;
using AuthorizationService.Service;
using AuthServices;
using AuthServices.Models;
using Microsoft.AspNetCore.Mvc;
using SharedLib;
using SharedLib.Utils;


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
        /// client khi login vao API se su dung ham nay
        /// tra ve KQ la JwtData
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            IActionResult response = Unauthorized();
            processResult = await _authenticationService.Login(model);
          
            if (processResult.OK)
            {
               
                response = Ok(processResult);
            }
           else
            {
                //dich message?
            }
            return response;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Logout")]
        [HttpPost]
        public IActionResult Logout(LogOutModel model)
        {
            var logutResult = _authenticationService.Logout(model);
            return Ok(logutResult);
        }
        /// <summary>
        /// khi can cung cap lai Access token thi dung ham nay
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("RenewToken")]
        [HttpPost]
        public IActionResult RenewToken(JwtData model)
        {
            var renewTokenResult = _authenticationService.RenewToken(model);
            return Ok(renewTokenResult);
        }
    }
}
