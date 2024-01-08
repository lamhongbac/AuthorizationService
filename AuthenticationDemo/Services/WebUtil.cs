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
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsLogin()
        {
            //bool debug= _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            if (_httpContextAccessor != null)
            {
                JwtData jwtData = _httpContextAccessor.HttpContext.Session.GetObject<JwtData>(AppConstants.JwtData);
                if (jwtData == null) return false;
                return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// luu them thong tin vao session ngoai thong tin dc luu boi framework
        /// _httpContextAccessor.HttpContext
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public bool SetLogin(LoginInfo loginInfo)
        {
            _httpContextAccessor.HttpContext.Session.SetString(AppConstants.LoginDate, loginInfo.LoginDate.ToString());
            _httpContextAccessor.HttpContext.Session.SetObject(AppConstants.JwtData, loginInfo.JwtData);
            return true;
        }
        /// <summary>
        /// remove cac thong tin da luu
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SetLogout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
            _httpContextAccessor.HttpContext.Session.Remove(AppConstants.LoginDate);
            _httpContextAccessor.HttpContext.Session.Remove(AppConstants.JwtData);
            return true;
        }
        public JwtData GetJwtData()
        {
            JwtData jwtData = _httpContextAccessor.HttpContext.Session.GetObject<JwtData>(AppConstants.JwtData);

            return jwtData;
        }


        public void SaveJwtData(JwtData data)
        {
            JwtData jwtData = _httpContextAccessor.HttpContext.Session.GetObject<JwtData>(AppConstants.JwtData);
            //if islogiDate !=null
            //
            _httpContextAccessor.HttpContext.Session.SetObject(AppConstants.JwtData, data);
        }
    }
}
