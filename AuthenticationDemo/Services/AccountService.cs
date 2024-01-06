using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using AuthenticationDemo.Models;
using SharedLib;
//using AuthServices.LoginInfo;
//using SharedLib.Models;
using AuthServices;
using AuthServices.Models;
using AuthServices.Util;
using System.Net.Http.Headers;
using System.Net.Http;

namespace AuthenticationDemo.Services
{
    public class AccountService
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _factory;
        const string strUrl = "";
        JwtUtil jwtUtil;
        public AccountService(IHttpClientFactory factory, 
            IHttpContextAccessor httpContextAccessor,
            JwtUtil jwtUtil)
        {
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
            this.jwtUtil = jwtUtil;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Login(LoginModel model)
        {
            HttpClient _httpClient = _factory.CreateClient("auth");
    //        httpClient.DefaultRequestHeaders.Authorization =
    //new AuthenticationHeaderValue("Bearer", "Your Oauth token");
            bool isLogin = false;

            // qua trinh login cua MVC
            //Cap nhat cookie information
            string json = JsonConvert.SerializeObject(model);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(strUrl, data);
            string result = await response.Content.ReadAsStringAsync();
            BODataProcessResult processResult = JsonConvert.DeserializeObject<BODataProcessResult>(result);

            isLogin = processResult != null && processResult.OK;

            //save cookier JwtData
            LoginInfo loginInfo =(LoginInfo)processResult.Content;




            //UserInfo userInfo = jwtUtil.GetUserInfo(loginInfo.JwtData.AccessToken);

            //List<Claim> claims = new List<Claim>()
            //{
            //        new Claim(ClaimTypes.NameIdentifier,userInfo.FullName),
            //        new Claim("Roles", string.Join(",", userInfo.Roles)),
            //        new Claim(ClaimTypes.Email,userInfo.EmailAddress),

            //    //new Claim(ClaimTypes.StreetAddress,account.Address)
            //};
            //ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsIdentity identity = jwtUtil.GetClaims(loginInfo.JwtData.AccessToken);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.KeepLogined
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);

            return isLogin;
        }

        public JwtData ReNewToken(JwtData jwtData)
        {
            //Cap nhat cookie information
            return new JwtData();
        }
    }
}
