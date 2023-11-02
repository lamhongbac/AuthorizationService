using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using AuthenticationDemo.Models;
using SharedLib;

namespace AuthenticationDemo.Services
{
    public class AccountService
    {
        IHttpContextAccessor _httpContextAccessor;
        HttpClient _httpClient;
        const string strUrl = "";
        public AccountService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task Login(LoginModel model)
        {


            // qua trinh login cua MVC
            //Cap nhat cookie information
            string json = JsonConvert.SerializeObject(model);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(strUrl, data);
            string result = await response.Content.ReadAsStringAsync();
            LoginInfo loginInfo = JsonConvert.DeserializeObject<LoginInfo>(result);

            //save cookier JwtData
            JwtUtil jwtUtil = new JwtUtil();
            UserInfo userInfo = jwtUtil.GetUserInfo(loginInfo.JwtData.Jwt);

            List<Claim> claims = new List<Claim>()
            {
                    new Claim(ClaimTypes.NameIdentifier,userInfo.FullName),
                    new Claim("Roles", string.Join(",", userInfo.Roles)),
                    new Claim(ClaimTypes.Email,userInfo.EmailAddress),

                //new Claim(ClaimTypes.StreetAddress,account.Address)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.KeepLogined
            };

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);
        }

        public JwtData ReNewToken(JwtData jwtData)
        {
            //Cap nhat cookie information
            return new JwtData();
        }
    }
}
