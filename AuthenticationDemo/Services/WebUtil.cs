using AuthenticationDemo.Library;
using AuthenticationDemo.Models;
using AuthServices;
using AuthServices.Models;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using SharedLib.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace AuthenticationDemo.Services
{
   
    public static class WebExtentions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        
    }
    public class WeUtils
    {
        IHttpContextAccessor _httpContextAccessor;
        public WeUtils(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

    }

    /// <summary>
    /// goi request va tra ve BODataProcess, kg tra ve Business object
    /// </summary>
    public class RequestHandler
    {
        private static RequestHandler instance;
        RequestHandler ()
        {

        }
        public static RequestHandler GetInstance()
        {
            if (instance == null)
                instance = new RequestHandler();

                return instance;

        }
        /// <summary>
        /// goi request va tra ve BODataProcess
        /// </summary>
        /// <param name="client"></param>
        /// <param name="strAccessURL"></param>
        /// <returns></returns>
        public async    Task<BODataProcessResult> GetRequest(HttpClient client,string strAccessURL, string? bearToken=null)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            string httpError = string.Empty;
            if (!string.IsNullOrWhiteSpace(bearToken))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearToken}");
            }
            var response = await client.GetAsync(strAccessURL);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                processResult = JsonConvert.DeserializeObject<BODataProcessResult>(result);
                                
            }
            else
            {
                processResult.OK = false;
                processResult.Content = null;
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
                        httpError = "UnAuthorized requirements";
                        break;
                    case EHttpStatusCode.Forbidden:
                        httpError = "Forbidden requirements";
                        break;
                    default:
                        break;
                }
                processResult.Message=httpError;
            }

            return processResult;
        }

        public async Task<BODataProcessResult> PostRequest(HttpClient client, string strAccessURL, object model, string bearToken=null)
        {

            BODataProcessResult processResult = new BODataProcessResult();
            string httpError = string.Empty;
            if (string.IsNullOrWhiteSpace(bearToken))
            {
                client.DefaultRequestHeaders.Remove("Authorization");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearToken}");
            }
            
            HttpContent jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(strAccessURL,jsonContent);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                processResult = JsonConvert.DeserializeObject<BODataProcessResult>(result);

            }
            else
            {
                processResult.OK = false;
                processResult.Content = null;
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
                        httpError = "UnAuthorized requirements";//renew token 
                        break;
                    case EHttpStatusCode.Forbidden:
                        httpError = "Forbidden requirements"; // not allow
                        break;
                    default:
                        break;
                }
                processResult.Message = httpError;
            }

            return processResult;
        }
    }
}
