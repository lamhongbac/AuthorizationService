using Microsoft.AspNetCore.Authorization;

namespace StudyApiAuth.Models
{
    /// <summary>
    /// doi tuong dung de 
    /// khai bao 1 object va cac quyen can thiet 
    /// 
    /// </summary>
    public class HasPermissionAttribute:AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) :base(permission.ToString())
        { 
        }
        //public List<string> ReqPermissions { get; set; }

        ////object Name as policyName
        //public HasPermissionAttribute(string objectName,string req_permissions) : base(objectName.ToString())
        //{
        //    ReqPermissions = new List<string>();

        //    foreach (var permission in req_permissions.Split(",").ToList() )
        //        { ReqPermissions.Add(permission.ToString()); }
            
        //}
       
    }
}
