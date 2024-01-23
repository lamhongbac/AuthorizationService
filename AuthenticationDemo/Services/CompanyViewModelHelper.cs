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
        AccountService _accountService;
        MSASignInManagerA _msaSignInManager;
        string bearToken;
        public CompanyViewModelHelper(IHttpClientFactory factory,
             IHttpContextAccessor httpContextAccessor,
             IConfiguration configuration, AccountService accountService,
              MSASignInManagerA msaSignInManager
             )
        {
            _msaSignInManager = msaSignInManager;
            _factory = factory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _serviceConfig = _configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
            _appConfig = _configuration.GetSection("AppConfig").Get<AppConfig>();
             _accountService = accountService;

        }
        public string BearToken { get=>bearToken; set =>bearToken=value; }
        public async Task<CompaniesViewModel> GetCompanies()
        {
            BODataProcessResult processResult = new BODataProcessResult();
            CompaniesViewModel viewModel = new CompaniesViewModel();
            HttpRequestHandler requestHandler = HttpRequestHandler.GetInstance();

            try
            {                
                JwtData jwtData  = _msaSignInManager.GetJwtData();
                

                if (jwtData != null)
                {
                    JwtData newJwtData = await _accountService.ReNewToken(jwtData);
                    if (newJwtData == null) {
                        viewModel.AddError("", "can not get access token");
                        return viewModel;
                    }
                    // kiem tra neu la new token==>??
                    bool isNewToken = jwtData.RefreshToken != newJwtData.RefreshToken;
                    
                   
                    HttpClient _httpClient = _factory.CreateClient(AppConstants.ProtectedResourceService);
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");                   
                    
                    if (newJwtData != null)
                    {
                        bearToken = newJwtData.AccessToken.ToString();                       
                        string strAccessURL = AppConstants.CompanyApiRoute + _serviceConfig.GetCompanies;                        
                        processResult = await requestHandler.GetRequest(_httpClient, strAccessURL, bearToken);
                        
                        if (processResult != null) 
                        {
                            if (processResult.Content != null && processResult.OK)
                            {
                                string result = JsonConvert.SerializeObject(processResult.Content);
                                List<CompanyViewModel> companyList = JsonConvert.DeserializeObject<List<CompanyViewModel>>(result);
                                viewModel.Companies = companyList;
                            }
                            else if(!processResult.OK)
                            {
                                viewModel.AddError("", processResult.Message);
                            }
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
