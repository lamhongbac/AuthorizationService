using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace AuthenticationDemo.Models
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string objectName = "company";

            //lay ra danh sach quyen tuong ung voi object tren doi tuong User


            Dictionary<string, List<string>> user_permissions = new Dictionary<string, List<string>>();
            List<string> permissions = new List<string>();

            var claims = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type.ToLower() == "objectrights");

            
            string userRights = claims.Value;

            if (!string.IsNullOrEmpty(userRights))
            {
                user_permissions = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(userRights);
                //userInfo.ObjectRights = user_permissions;
                if (user_permissions != null && user_permissions.ContainsKey(objectName))
                    permissions = user_permissions[objectName];
            }



            // yc de dc phep (authorized)
            List<string> requirementPermission = allowedroles.ToList();
            //kiem tra xem quyen cua user dang co , co thỏa mãn hay kg?
            bool authorized = false;

            if (requirementPermission == null)
            {
                authorized = true;
            }
            else
            {


                foreach (var permission in permissions)
                {
                    if (requirementPermission.Contains(permission))
                    {
                        authorized = true;
                        break;
                    }
                }
            }
            if (!authorized)
            {

                context.Result = new UnauthorizedObjectResult(string.Empty);
            }
            return;

        }

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    bool authorize = false;
        //    foreach (var role in allowedroles)
        //    {
        //        var user = context.AppUser.Where(m => m.UserID == GetUser.CurrentUser/* getting user form current context */ && m.Role == role &&
        //        m.IsActive == true); // checking active users with allowed roles.  
        //        if (user.Count() > 0)
        //        {
        //            authorize = true; /* return true if Entity has current user(active) with specific role */
        //        }
        //    }
        //    return authorize;
        //}
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new HttpUnauthorizedResult();
        //}


    }
}
