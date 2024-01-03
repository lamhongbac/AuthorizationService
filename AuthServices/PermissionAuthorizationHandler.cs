using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServices
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement">la thuoc tinh tren action (objectName, rights)</param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            string objectName = requirement.ObjectName.ToString(); //company

            //lay ra danh sach quyen tuong ung voi object tren doi tuong User

            var claims = context.User.Claims.Where(x => x.Type.ToLower() == objectName.ToLower());
            Dictionary<string, List<string>> user_permissions = new Dictionary<string, List<string>>();
            List<string> permissions = new List<string>();
            string userRights = context.User.Claims.First(x => x.Type.ToLower() == "objectrights").Value;
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
            foreach (var permission in permissions)
            {
                if (requirement.RequiredPermissions.Contains(permission))
                {
                    authorized = true;
                    break;
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
