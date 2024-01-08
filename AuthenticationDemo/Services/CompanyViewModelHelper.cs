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

                JwtData jwtData = new JwtData();// _webUtils.GetJwtData();
                
                JwtClientUtil jwtClientUtil=new JwtClientUtil();

                // string bearToken= jwtData.AccessToken.ToString();
                string bearToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjZTA4NDY0NC0wMzY0LTQ1MzMtYThlOS1lYWI0MmJkMzZkOGUiLCJVc2VyTmFtZSI6ImFkbWluIiwic3ViIjoiYWRtaW4iLCJVc2VySUQiOiI3IiwiRnVsbE5hbWUiOiJQaOG6oW0gSG_DoG5nIMSQ4bqhdCIsIkFwcElEIjoiMyIsIkNvbXBhbnlJRCI6IjMiLCJSb2xlcyI6InFhbWFuYWdlciIsIk9iamVjdFJpZ2h0cyI6IntcImFwcHJvbGVcIjpbXCJyZWFkXCIsXCJjcmVhdGVcIixcInVwZGF0ZVwiLFwiZGVsZXRlXCJdLFwiYXBwdXNlclwiOltcInJlYWRcIixcImNyZWF0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDAxXCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDAyXCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDA0XCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDAzXCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDA1XCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDA2XCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImNvbXBhbnlcIjpbXCJyZWFkXCIsXCJjcmVhdGVcIl19IiwibmJmIjoxNzA0Njk5OTM2LCJleHAiOjE3MDQ3MDAwNTYsImlhdCI6MTcwNDY5OTkzNiwiaXNzIjoibXNhLmNvbS52biIsImF1ZCI6Im1zYS5jb20udm4ifQ.9P8cJm-ztfeuUPfL2P0AyvqfarG7IjaZmCHeQXxsZXk";
                //{
                //    jwtData=await _accountService.ReNewToken(jwtData);
                //    _webUtils.SaveJwtData(jwtData);
                //    bearToken = jwtData.AccessToken.ToString();
                //}
                //else
                //{
                //    // kg can lam gi ca
                //}

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
                            debugError = "UnAuthorized requirements";
                            break;
                        case EHttpStatusCode.Forbidden:
                            debugError = "Forbidden requirements";
                            break;
                        default:
                            break;
                    }
                    return companies;
                }
            }
            catch (Exception ex)
            {
                debugError = "Bad Request";
            }
            return companies;
        }
    }
}
