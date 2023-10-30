using AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Login(LoginModel model)
        {
            return Ok(new LoginInfo());
        }
        public bool Logout(LogOutModel model) 
        { 
            return false;
        }
        public bool ChangePwd(ChangePwdModel model) {
            
            return false;
        }
        public bool UpdateProfile(ChangePwdModel model)
        {

            return false;
        }
    }
}
