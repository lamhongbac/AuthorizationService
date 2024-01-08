using AuthenticationDemo.Library;
using AuthServices.Models;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AuthenticationDemo.Services
{
   
    public static class WebExtentions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        
    }
    public class WeUtils
    {
        IHttpContextAccessor _httpContextAccessor;
        public WeUtils(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor= httpContextAccessor;
        }
        public  bool IsLogin()
        {
            //bool debug= _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            if (_httpContextAccessor != null)
            {
                LoginInfo loginInfo = _httpContextAccessor.HttpContext.Session.GetObject<LoginInfo>(AppConstants.LoginInfo);
                if (loginInfo == null) return false;
                return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
            else
            {
                return false;
            }
        }
        public bool SetLogin(LoginInfo loginInfo)
        {
            _httpContextAccessor.HttpContext.Session.SetObject(AppConstants.LoginInfo, loginInfo);
            return true;
        }
        public async Task<bool> SetLogout()
        {
          await  _httpContextAccessor.HttpContext.SignOutAsync();
            _httpContextAccessor.HttpContext.Session.Remove(AppConstants.LoginInfo);
            return true;
        }
        public JwtData GetJwtData() 
        {
            LoginInfo loginInfo = _httpContextAccessor.HttpContext.Session.GetObject<LoginInfo>(AppConstants.LoginInfo);
            JwtData jwtData = loginInfo.JwtData;
            return jwtData;
        }
    }
}
