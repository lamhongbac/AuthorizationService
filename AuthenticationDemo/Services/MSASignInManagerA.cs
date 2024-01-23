using AuthenticationDemo.Library;
using AuthenticationDemo.Models;
using AuthServices.Models;
using AuthServices.Util;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace AuthenticationDemo.Services
{
    public  class MSASignInManagerA 
    {
        
        IHttpContextAccessor _contextAccessor;
      static  MSAUserInfo _userInfo;
        public MSASignInManagerA(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor= contextAccessor;
           
        }
        
        public bool IsSignedIn()
        {
            //return base.IsSignedIn(principal);

            if (_contextAccessor != null)
            {
                JwtData jwtData = _contextAccessor.HttpContext.Session.GetObject<JwtData>(AppConstants.JwtData);
                if (jwtData == null) return false;
                return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
            else
            {
                return false;
            }


        }
        /// <summary>
        /// kiem tra role co trong logined user
        /// </summary>
        /// <param name="strRoles">role set tren view</param>
        /// <returns></returns>
        public  bool HasPermission(string strRoles)
        {
            List<string> roles = strRoles.Split(";").ToList();
            string objectName = roles[0]; //company
            List<string> required=roles[1].Split(",").ToList();

            //gia su sau khi check ra perm cua user la 
            List<string> userRights= new List<string>() { "admin", "user" };
            bool authorized = false;
            foreach (string right in userRights)
            {
                if (required.Contains(right))
                {
                    authorized = true;
                    break;
                }
            }
            return authorized;
        }
        public  async Task SignInAsync(MSAUserInfo userInfo)
        {
            if (_userInfo == null)
                _userInfo= new MSAUserInfo();

            _userInfo = userInfo;



            LoginInfo loginInfo = userInfo.LoginInfo;



            //===>Jwt client process
            JwtClientUtil jwtUtil = new JwtClientUtil();
            List<Claim> claims = jwtUtil.GetClaims(loginInfo.JwtData.AccessToken);
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = userInfo.KeepLogined
            };

            //====login into HttpContext
            ClaimsPrincipal user = new ClaimsPrincipal(identity);

            _contextAccessor.HttpContext.SignInAsync(user, authenticationProperties);


            _contextAccessor.HttpContext.User = user;

            //===>save cookier JwtData
            SetLogin(_userInfo.LoginInfo);

        }


      
        /// <summary>
        /// luu them thong tin vao session ngoai thong tin dc luu boi framework
        /// _httpContextAccessor.HttpContext
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        private bool SetLogin(LoginInfo loginInfo)
        {
            bool OK = false;

            try
            {
                OK = loginInfo != null;
                _contextAccessor.HttpContext.Session.SetObject(AppConstants.LoginDate, loginInfo.LoginDate);
                _contextAccessor.HttpContext.Session.SetObject(AppConstants.JwtData, loginInfo.JwtData);
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
            return OK;
        }
        /// <summary>
        /// remove cac thong tin da luu
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SetLogout()
        {
            await _contextAccessor.HttpContext.SignOutAsync();
            _contextAccessor.HttpContext.Session.Remove(AppConstants.LoginDate);
            _contextAccessor.HttpContext.Session.Remove(AppConstants.JwtData);
            return true;
        }
        public JwtData GetJwtData()
        {
            string loginDate = _contextAccessor.HttpContext.Session.GetString(AppConstants.LoginDate);
            JwtData jwtData = null;
            jwtData = _contextAccessor.HttpContext.Session.GetObject<JwtData>(AppConstants.JwtData);
            if (!string.IsNullOrWhiteSpace(loginDate))
            {
                jwtData = _contextAccessor.HttpContext.Session.GetObject<JwtData>(AppConstants.JwtData);
                return jwtData;
            }
            return null;
        }

        /// <summary>
        /// Save JwtData when renew token only
        /// </summary>
        /// <param name="data"></param>
        public bool SaveJwtData(JwtData data)
        {
            string loginDate = _contextAccessor.HttpContext.Session.GetString(AppConstants.LoginDate);

            if (!string.IsNullOrWhiteSpace(loginDate))
            {
                _contextAccessor.HttpContext.Session.SetObject(AppConstants.JwtData, data);
                return true;
            }
            return false;
        }

        public async Task SignOutAsync()
        {
            await SetLogout();
        }
    }
}
