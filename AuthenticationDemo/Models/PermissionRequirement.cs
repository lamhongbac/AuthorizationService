﻿using Microsoft.AspNetCore.Authorization;

namespace AuthenticationDemo.Models
{
    /// <summary>
    /// mo ta cac quyen can thiet  cho 1 object:
    /// ObjectName/RequiredPermissions
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="rights"></param>
        public PermissionRequirement(string objectName, string rights)
        {
            ObjectName = objectName;

            RequiredPermissions = rights.Split(',').ToList();
        }
        public string ObjectName { get; set; }
        public List<string> RequiredPermissions { get; set; }
    }
}
