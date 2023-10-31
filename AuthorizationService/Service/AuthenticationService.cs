using AuthorizationService.Data;
using AuthServices;
using SharedLib.Models;

namespace AuthorizationService.Service
{
    public class AuthenticationService
    {
        internal UserInfo? AuthenticateUser(LoginModel model)
        {
            throw new NotImplementedException();
        }

        internal JwtData GenerateJSONWebToken(UserInfo user)
        {
            throw new NotImplementedException();
        }

        internal object Logout(LogOutModel model)
        {
            throw new NotImplementedException();
        }

        internal object RenewToken(JwtData model)
        {
            throw new NotImplementedException();
        }
    }
}
