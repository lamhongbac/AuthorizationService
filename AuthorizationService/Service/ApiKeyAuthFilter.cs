using AuthorizationService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace AuthorizationService.Service
{
    /// <summary>
    /// Service.AddControllers(x=>x.Filter.Add[ApiKeyAuthFilter]
    /// tuong duong su dung middle ware
    /// we su dung pp cuoi cung la AuthorizedFilter, de co the tuy bien
    /// co the them Attribute=>Attribute,IAuthorizationFilter
    /// de co the su dung direct filter nhu la [ApiKeyAuthFilter] , kg can qua kg can service filter
    /// </summary>
    /// 
    public class ApiKeyAuthFilter :  IAuthorizationFilter
    {
        IConfiguration configuration;

        // khi dung nhu attribute thi kg can service filter
        // va kg the inject configuration dc

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext Acontext)
        {
            HttpContext context=Acontext.HttpContext;

            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeader, out var extractedKey))
            {
                Acontext.Result=new UnauthorizedObjectResult("API Key Missing");
                //await context.Response.WriteAsync("API Key Missing");
                return;
            }
           
            configuration = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiConfigSection = configuration.GetSection(AuthConstants.ApiKeySection);
            

            ApiKeyObject apiKeyObject = null;
            if (apiConfigSection != null)
            {
                List<ApiKeyObject> apikeys = apiConfigSection.Get<List<ApiKeyObject>>();
                apiKeyObject = apikeys.FirstOrDefault(x => x.ApiKey == extractedKey);
            }
            if (apiKeyObject == null)
            {
                Acontext.Result = new UnauthorizedObjectResult("API Key Invalid");
                //await context.Response.WriteAsync("API Key Invalid");
                return;
            }
            //await _requestDelegate(context);
        }
    }
}
