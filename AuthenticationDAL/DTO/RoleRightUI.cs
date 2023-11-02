using Dapper.Contrib.Extensions;
using System;

namespace AuthenticationDAL.DTO
{
    [Table("RoleRights")]
    public class RoleRightUI:BaseUI
    {
        [Key]
        public int Id { get; set; }
        public int RoleID { get; set; }
        public int AppObjectID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public string CreatedBy { get; set; }
    }
}
