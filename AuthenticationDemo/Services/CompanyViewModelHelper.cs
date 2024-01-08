using AuthenticationDemo.Library;
using AuthenticationDemo.Models;
using AuthServices.Models;
using AuthServices;
using Newtonsoft.Json;
using SharedLib.Models;
using System.Reflection;
using System.Text;
using AuthServices.Util;

namespace AuthenticationDemo.Services
{
    /// <summary>
    /// company service
    /// </summary>
    public class CompanyViewModelHelper
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _factory;
        IConfiguration _configuration;
        ServiceConfig _serviceConfig;
        AppConfig _appConfig;
        WeUtils _webUtils;
        AccountService _accountService;
        public CompanyViewModelHelper(IHttpClientFactory factory,
             IHttpContextAccessor httpContextAccessor,
             IConfiguration configuration, WeUtils webUtils,
             AccountService accountService
             )
        {
            _accountService= accountService;
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _serviceConfig = _configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
            _appConfig = _configuration.GetSection("AppConfig").Get<AppConfig>();
            _webUtils = webUtils;

        }
        public async Task<List<CompanyViewModel>> GetCompanies()
        {
            string debugError = string.Empty;
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            try
            {
                HttpClient _httpClient = _factory.CreateClient(AppConstants.ProtectedResourceService);
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                
                JwtData jwtData = _webUtils.GetJwtData();
                
                JwtClientUtil jwtClientUtil=new JwtClientUtil();
                
                string bearToken= jwtData.AccessToken.ToString();

                if (!jwtClientUtil.IsAccessTokenExpired(jwtData.AccessToken))
                {
                    jwtData=await _accountService.ReNewToken(jwtData);
                    _webUtils.SaveJwtData(jwtData);
                    bearToken = jwtData.AccessToken.ToString();
                }
                else
                {
                    // kg can lam gi ca
                }

                _httpClient.DefaultRequestHeaders.Add("Authorization", bearToken);
                string strAccessURL = AppConstants.CompanyApiRoute + _serviceConfig.GetCompanies;
              
                BODataProcessResult processResult = new BODataProcessResult(); ;
                //===>call api===>
               
                var response = await _httpClient.GetAsync(strAccessURL);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    processResult = JsonConvert.DeserializeObject<BODataProcessResult>(result);
                    if (processResult.OK)
                    {
                        companies = JsonConvert.DeserializeObject<List<CompanyViewModel>>(processResult.Content.ToString()); //(LoginInfo)processResult.Content;

                    }
                    return companies;
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
                            debugError = "Invalid token requirements";
                            break;
                        case EHttpStatusCode.Forbidden:
                            break;
                        default:
                            break;
                    }
                    return companies;
                }
            }
            catch (Exception ex)
            {
            }
            return companies;
        }
    }
}
