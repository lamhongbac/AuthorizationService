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
        public LoginInfo Login(LoginModel model)
        {
            return new LoginInfo();
        }
        public bool Logout(LogOutModel model) 
        { 
            return false;
        }
        public bool ChangePwd(ChangePwdModel model) {
            
            return false;
        }
    }
}
