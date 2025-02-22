using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices.Models
{
    /// <summary>
    /// cung cap quyen cua user theo to chuc
    /// Danh sach object+right -- Role, khac phuc loi role dang doc lap khoi right
    /// 
    /// </summary>
    public class UserRight
    {
        public UserRight()
        {
            ObjectRights = new List<ObjectRight>();
        }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<ObjectRight> ObjectRights { get; set; }
    }
}
