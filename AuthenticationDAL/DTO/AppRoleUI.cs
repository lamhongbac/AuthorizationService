using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("AppRoles")]
    public class AppRoleUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public int CompanyAppID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsStoreAdmin { get; set; }
    }
}
