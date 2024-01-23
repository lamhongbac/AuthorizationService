using AuthServices.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationDemo.Models
{
    public class MSAUserInfo : IdentityUser
    {
        public MSAUserInfo()
        {
            loginInfo = new LoginInfo();
        }
        LoginInfo loginInfo;
        public LoginInfo LoginInfo { get => loginInfo; set => loginInfo = value; }
    }
}
