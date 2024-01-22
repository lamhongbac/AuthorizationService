using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace AuthenticationDemo.Models
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// tu user claim doi tuong objectrights=> ep kieu ve dictionary (user right object)
        /// sau do kiem tra voi qui dinh tren action /auth attribute
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement">la thuoc tinh right can thiet de access  actionmethod 
        /// no dung de so sanh voi user right trong context.User </param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            string objectName = requirement.ObjectName.ToString(); //company

            //lay ra danh sach quyen tuong ung voi object tren doi tuong User


            Dictionary<string, List<string>> user_permissions = new Dictionary<string, List<string>>();
            List<string> permissions = new List<string>();

            var claims = context.User.Claims.FirstOrDefault(x => x.Type.ToLower() == "objectrights");

            if (claims == null)
            {
                context.Fail(); return Task.CompletedTask;
            }
            string userRights = claims.Value;

            if (!string.IsNullOrEmpty(userRights))
            {
                user_permissions = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(userRights);
                //userInfo.ObjectRights = user_permissions;
                if (user_permissions != null && user_permissions.ContainsKey(objectName))
                    permissions = user_permissions[objectName];
            }


            if (permissions.Count == 0)
            {
                context.Fail(); return Task.CompletedTask;
            }
            // yc de dc phep (authorized)
            List<string> requirementPermission = requirement.RequiredPermissions;
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
                    if (requirement.RequiredPermissions.Contains(permission))
                    {
                        authorized = true;
                        break;
                    }
                }
            }
            if (authorized)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
