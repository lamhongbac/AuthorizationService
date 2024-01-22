using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AuthenticationDemo.Models
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {

        AuthorizationOptions _options;
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options
            ) : base(options)
        {

            _options = options.Value;
        }
        /// <summary>
        /// duoc hieu nhu la noi doc input tu attribute
        /// 
        /// </summary>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string permission)
        {
            string[] object_permission = permission.Split(";").ToArray();

            //kiem tra ...
            string objectName = object_permission[0];
            string rights = object_permission[1];
            var policy=await base.GetPolicyAsync(objectName);
            //hardcode tam thoi
            //List<string> abc =((HasPermissionAttribute) _options).ReqPermissions;


            if (policy != null)
            {
                return policy;
            }
            
            

            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(objectName, rights))
                .Build();
        }
    }
}
