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
using Microsoft.Extensions.Configuration;
using AuthenticationDemo.Library;

namespace AuthenticationDemo.Services
{
    /// <summary>
    /// cung cap dich vu ve tai khoan cho ung dung
    /// </summary>
    public class AccountService
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _factory;
        IConfiguration _configuration;
        ServiceConfig _serviceConfig;
        AppConfig _appConfig;
        public AccountService(IHttpClientFactory factory,
            IHttpContextAccessor httpContextAccessor, IConfiguration configuration
            )
        {
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _serviceConfig = _configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
            _appConfig = _configuration.GetSection("AppConfig").Get<AppConfig>();


        }
        /// <summary>
        ///  httpClient.DefaultRequestHeaders.Authorization =
        ///  new AuthenticationHeaderValue("Bearer", "Your Oauth token");
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Login(LoginViewModel viewModel)
        {
            bool isLogin = false;
            LoginModel model = new LoginModel()
            {
                AppID = _appConfig.AppID,
                CompanyID = _appConfig.CompanyID,
                KeepLogined = viewModel.KeepLogined,
                Password = viewModel.Password,
                UserName = viewModel.UserName,
                UserType = "UserID",

            };
            try
            {
                HttpClient _httpClient = _factory.CreateClient("auth");
                string strLoginURL = AppConstants.AccountApiRoute+_serviceConfig.Login;

                //===>call api===>
                string json = JsonConvert.SerializeObject(model);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(strLoginURL, data);
                string result = await response.Content.ReadAsStringAsync();
                BODataProcessResult processResult = JsonConvert.DeserializeObject<BODataProcessResult>(result);
                isLogin = processResult != null && processResult.OK;
                LoginInfo loginInfo = JsonConvert.DeserializeObject<LoginInfo>(processResult.Content.ToString()); //(LoginInfo)processResult.Content;

                if (!isLogin)
                {
                    return isLogin;                
                }
                string strLoginInfo = JsonConvert.SerializeObject(loginInfo).ToString();
                //===>save cookier JwtData
                _httpContextAccessor.HttpContext.Session.SetObject(AppConstants.LoginInfo, strLoginInfo);
                //===>demo call object from session
                loginInfo = _httpContextAccessor.HttpContext.Session.GetObject<LoginInfo>(AppConstants.LoginInfo);


                //===end call api=========>

                JwtClientUtil jwtUtil = new JwtClientUtil();
                List<Claim> claims = jwtUtil.GetClaims(loginInfo.JwtData.AccessToken);
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.KeepLogined
                };

                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
            }catch(Exception ex)
            {
                string err = ex.Message;
            }

            return isLogin;
        }

        public JwtData ReNewToken(JwtData jwtData)
        {
            //Cap nhat cookie information
            return new JwtData();
        }
    }
}
