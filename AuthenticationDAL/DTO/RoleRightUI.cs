using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("RoleRights")]
    public class RoleRightUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int AppObjectID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
