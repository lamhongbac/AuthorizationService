using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("UserRoles")]
    public class UserRoleUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public int CompanyAppID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
