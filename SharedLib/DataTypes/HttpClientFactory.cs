//using AuthServices;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace MSASharedLib.Utils
//{
//    public class HttpClientFactory
//    {


//        private readonly IHttpClientFactory _httpClientFactory;

//        public HttpClientFactory(IHttpClientFactory httpClientFactory)
//        {
//            _httpClientFactory = httpClientFactory;

//        }
//        //
//        protected JsonSerializerOptions DefaultSerializerOptions =
//            new JsonSerializerOptions()
//            {
//                PropertyNameCaseInsensitive = true,
//                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
//            };



//        protected TData Deserialize<TData>(string jsonData) =>
//            JsonSerializer.Deserialize<TData>(jsonData, DefaultSerializerOptions);

//        protected async Task<TData> HandleApiResponseAsync<TData>(HttpResponseMessage response, TData defaultValue)
//        {
//            if (response.IsSuccessStatusCode)
//            {
//                var content = await response.Content.ReadAsStringAsync();
//                if (!string.IsNullOrEmpty(content))
//                {
//                    return Deserialize<TData>(content);
//                }
//            }
//            return defaultValue;
//        }

//        protected async Task<HttpResponseMessage> PostRequest(object request, string strApiUrl, string serviceName)
//        {
//            HttpClient HttpClient = _httpClientFactory.CreateClient(serviceName);
//            string json = JsonSerializer.Serialize(request);
//            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
//            return await HttpClient.PostAsync(strApiUrl, data);
//        }
//        protected async Task<BODataProcessResult> PostRequest(object request, string strApiUrl, string serviceName, BODataProcessResult defaultValue)
//        {
//            HttpClient HttpClient = _httpClientFactory.CreateClient(serviceName);
//            string json = JsonSerializer.Serialize(request);
//            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
//            HttpResponseMessage response = await HttpClient.PostAsync(strApiUrl, data);

//            if (response.IsSuccessStatusCode)
//            {
//                var content = await response.Content.ReadAsStringAsync();
//                if (!string.IsNullOrEmpty(content))
//                {
//                    BODataProcessResult result = Deserialize<BODataProcessResult>(content);
//                    return result;

//                    //JsonSerializer.Deserialize<TData>(jsonData, DefaultSerializerOptions); ;
//                }
//            }
//            return defaultValue;
//        }
//    }
//}
