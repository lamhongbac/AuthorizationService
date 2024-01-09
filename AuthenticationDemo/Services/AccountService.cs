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
        WeUtils _webUtils;
        public AccountService(IHttpClientFactory factory,
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration, WeUtils webUtils
            )
        {
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _serviceConfig = _configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
            _appConfig = _configuration.GetSection("AppConfig").Get<AppConfig>();
            _webUtils= webUtils;

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
                HttpClient _httpClient = _factory.CreateClient(AppConstants.AuthenticationService);
                string strAcessURL = AppConstants.AccountApiRoute + _serviceConfig.Login;
                LoginInfo loginInfo = null;
                BODataProcessResult processResult = new BODataProcessResult(); ;
                //===>call api===>
                string json = JsonConvert.SerializeObject(model);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(strAcessURL, data);
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
                _webUtils.SetLogin(loginInfo);
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
                //debug
                isLogin = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

                _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(identity);
                
                //debug
                 isLogin= _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

                return isLogin;
                //===>

            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }
            //===> end login process
            return isLogin;
        }

        /// <summary>
        /// renew token base on exist token
        /// neu renew thanh cong thi tra ve new token
        /// neu renew that bai tra ve null
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<JwtData?> GetAccessToken(JwtData model)
        {
            //Cap nhat cookie information
           
            JwtClientUtil jwtClientUtil = new JwtClientUtil();
            if (jwtClientUtil.IsAccessTokenExpired(model.AccessToken))
            {

                return model;
            }

            JwtData reNewToken = null;
            try
            {
                HttpClient _httpClient = _factory.CreateClient("auth");
                string strRenewTokenURL = AppConstants.AccountApiRoute + _serviceConfig.RenewToken;
                
                BODataProcessResult processResult = new BODataProcessResult(); 
                //===>call api===>
                string json = JsonConvert.SerializeObject(model);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(strRenewTokenURL, data);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    processResult = JsonConvert.DeserializeObject<BODataProcessResult>(result);
                    if (processResult != null && processResult.OK)
                    {
                        reNewToken = JsonConvert.DeserializeObject<JwtData>(processResult.Content.ToString()); //(LoginInfo)processResult.Content;
                    }
                    
                    if (reNewToken!=null)
                        _webUtils.SaveJwtData(reNewToken);
                    return reNewToken;
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
                    return null;
                }
                //===end call api=========>
                
              

               
            }
            catch (Exception ex)
            {
                reNewToken = null;
                string err = ex.Message;
            }
            //===> end login process
            return reNewToken;
        }

        public async Task<bool> Logout()
        {
           return await _webUtils.SetLogout();
        }
    }
}
