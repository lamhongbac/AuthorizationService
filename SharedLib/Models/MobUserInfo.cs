using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthSharedLib.Models
{
    public class MobUserInfo
    {
        public MobUserInfo()
        {
            Roles = new List<string>();
            StoreIDs = new List<int>();
            ObjectRights = new List<ObjectRight>();
        }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public List<string> Roles { get; set; }
        public List<ObjectRight> ObjectRights { get; set; }
        public int AppID { get; set; }
        public int CompanyID { get; set; }
        public int ManagerID { get; set; }
        public string ManagerEmail { get; set; }
        public List<int> StoreIDs { get; set; }
        public Guid LoginID { get; set; }
        public DateTime LoginDate { get; set; }

    }

    public struct ObjectRight
    {
        public string ObjectName { get; set; }
        public List<string> Rights { get; set; }
    }
}
