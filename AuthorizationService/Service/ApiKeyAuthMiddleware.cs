using AuthorizationService.DataTypes ;

namespace AuthorizationService.Service
{
    /// <summary>
    /// app.UseMiddleWare[ApiKeyAuthMiddleware]()
    /// pp nay moi request deu phai co api key hop le
    /// 
    /// </summary>
    public class ApiKeyAuthMiddleware
    {

        private RequestDelegate _requestDelegate;
        IConfiguration _configuration;
        public ApiKeyAuthMiddleware(RequestDelegate requestDelegate, IConfiguration configuration)
        {
            _requestDelegate = requestDelegate;
            _configuration = configuration;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeader,out var extractedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key Missing");
                return;
            }
            var key = extractedKey;
            var apiConfigSection=_configuration.GetSection(AuthConstants.ApiKeySection);
            ApiKeyObject apiKeyObject = null;
            if (apiConfigSection != null)
            {
                List<ApiKeyObject> apikeys = apiConfigSection.Get<List<ApiKeyObject>>();
                apiKeyObject=apikeys.FirstOrDefault(x=>x.ApiKey==key);
            }
            if (apiKeyObject == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key Invalid");
                return;
            }
            await _requestDelegate(context);
        }
    }
}
