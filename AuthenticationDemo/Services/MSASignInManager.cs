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
    public  class MSASignInManager : SignInManager<MSAUserInfo>
    {
        WeUtils _webUtils;
        IHttpContextAccessor _contextAccessor;
        LoginInfo _loginInfo;
        public MSASignInManager(UserManager<MSAUserInfo> userManager, IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<MSAUserInfo> claimsFactory, IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<MSAUserInfo>> logger, IAuthenticationSchemeProvider schemes,
            IUserConfirmation<MSAUserInfo> confirmation) : 
            base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _contextAccessor= contextAccessor;
           
        }
        public override bool IsSignedIn(ClaimsPrincipal principal)
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
        public  bool IsInRole(string[] roles)
        {
            List<string> loginedroles = new List<string>() { "admin", "ops" };
            bool isInRole = false;
            foreach (string role in loginedroles)
            {
                if (roles.Contains(role))
                {
                    isInRole =true;
                    break;
                }
            }
            return isInRole;
        }
        public override async Task SignInAsync(MSAUserInfo loginInfo, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {

            await base.SignInAsync(loginInfo, authenticationProperties);

            //===>save cookier JwtData
            SetLogin(loginInfo.LoginInfo);
            
            
            //===>demo call object from session
            //loginInfo = _httpContextAccessor.HttpContext.Session.GetObject<LoginInfo>(AppConstants.LoginInfo);

            //===>Jwt client process
            JwtClientUtil jwtUtil = new JwtClientUtil();
            List<Claim> claims = jwtUtil.GetClaims(loginInfo.LoginInfo.JwtData.AccessToken);
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            //====login into HttpContext
           

            //debug


            _contextAccessor.HttpContext.User = new ClaimsPrincipal(identity);

        }


      
        /// <summary>
        /// luu them thong tin vao session ngoai thong tin dc luu boi framework
        /// _httpContextAccessor.HttpContext
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public bool SetLogin(LoginInfo loginInfo)
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
        public async Task<bool> SetLogout()
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

    }
}
