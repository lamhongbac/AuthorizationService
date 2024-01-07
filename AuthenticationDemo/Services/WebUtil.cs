using Newtonsoft.Json;
using System.Security.Claims;

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
            _httpContextAccessor= httpContextAccessor;
        }
        public  bool IsLogin()
        {
            if (_httpContextAccessor != null)
            {
                return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
            return false;
        }
    }
}
