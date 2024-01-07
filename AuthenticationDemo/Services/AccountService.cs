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
using SharedLib.Models;

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
                string strLoginURL = AppConstants.AccountApiRoute + _serviceConfig.Login;
                LoginInfo loginInfo = null;
                BODataProcessResult processResult = new BODataProcessResult(); ;
                //===>call api===>
                string json = JsonConvert.SerializeObject(model);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(strLoginURL, data);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    processResult = JsonConvert.DeserializeObject<BODataProcessResult>(result);
                    isLogin = processResult.Content != null && processResult.OK;
                }
                else
                {
                    //xu ly loi http response status
                    EHttpStatusCode httpStatus = (EHttpStatusCode)response.StatusCode;
                    switch (httpStatus)
                    {
                        case EHttpStatusCode.Moved:
                            break;
                        case EHttpStatusCode.OK:
                            break;
                        case EHttpStatusCode.Redirect:
                            break;
                        case EHttpStatusCode.UnAuthorized:
                            break;
                        case EHttpStatusCode.Forbidden:
                            break;
                        default:
                            break;
                    }
                    return isLogin;
                }
                //===end call api=========>

                if (!isLogin )
                {
                    return isLogin;
                }
                
                //==> IsLogin=true Client Login process====>

                loginInfo = JsonConvert.DeserializeObject<LoginInfo>(processResult.Content.ToString()); //(LoginInfo)processResult.Content;
                
                //===>save cookier JwtData
                _httpContextAccessor.HttpContext.Session.SetObject(AppConstants.LoginInfo, loginInfo);
                
                //===>demo call object from session
                //loginInfo = _httpContextAccessor.HttpContext.Session.GetObject<LoginInfo>(AppConstants.LoginInfo);

                //===>Jwt client process
                JwtClientUtil jwtUtil = new JwtClientUtil();
                List<Claim> claims = jwtUtil.GetClaims(loginInfo.JwtData.AccessToken);
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.KeepLogined
                };
                
                //====login into HttpContext
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);
                _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(identity);
                return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                //===>

            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }
            //===> end login process
            return isLogin;
        }

        public JwtData ReNewToken(JwtData jwtData)
        {
            //Cap nhat cookie information
            return new JwtData();
        }
    }
}
