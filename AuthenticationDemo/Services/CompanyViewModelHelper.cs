using AuthenticationDemo.Library;
using AuthenticationDemo.Models;
using AuthServices.Models;
using AuthServices;
using Newtonsoft.Json;
using SharedLib.Models;
using System.Reflection;
using System.Text;
using AuthServices.Util;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Collections.Generic;

namespace AuthenticationDemo.Services
{
    /// <summary>
    /// company viewmodel helper
    /// lop nay co nhiem vu tra ve viewmodel cho controller
    /// neu co error thi add error vao model state
    /// 
    /// note:
    /// kg dc tra ve BODataProcess
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
        string bearToken;
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
        public string BearToken { get=>bearToken; set =>bearToken=value; }
        public async Task<CompaniesViewModel> GetCompanies()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            CompaniesViewModel viewModel = new CompaniesViewModel();
            RequestHandler requestHandler = RequestHandler.GetInstance();

            try
            {                
                JwtData jwtData  =_webUtils.GetJwtData();                
                if (jwtData != null)
                {
                    HttpClient _httpClient = _factory.CreateClient(AppConstants.ProtectedResourceService);
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");                   
                    JwtData newJwtData = await _accountService.ReNewToken(jwtData);
                    if (newJwtData != null)
                    {
                        bearToken = newJwtData.AccessToken.ToString();                       
                        string strAccessURL = AppConstants.CompanyApiRoute + _serviceConfig.GetCompanies;                        
                        processResult = await requestHandler.GetRequest(_httpClient, strAccessURL, bearToken);
                        
                        if (processResult != null && processResult.Content != null && processResult.OK) 
                        {
                            string result = JsonConvert.SerializeObject(processResult.Content);
                            List<CompanyViewModel> companyList = JsonConvert.DeserializeObject<List<CompanyViewModel>>(result);
                            viewModel.Companies = companyList;
                        }
                        else
                        {
                            string fieldName="";
                            string errMessage = processResult.Message;

                            viewModel.AddError(fieldName, errMessage);
                        }
                        
                        
                    }
                    else
                    {
                        string fieldName = "";
                        string errMessage = "Invalid renew JwtData";

                        viewModel.AddError(fieldName, errMessage);
                    }

                }
                else
                {
                    string fieldName = "";
                    string errMessage = "Invalid current JwtData";

                    viewModel.AddError(fieldName, errMessage);
                }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
                string fieldName = "";
                string errMessage = err;

                viewModel.AddError(fieldName, errMessage);
            }
            return viewModel;
        }
    }
}
