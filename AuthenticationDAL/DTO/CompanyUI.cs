using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("Companies")]
    public class CompanyUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public string RegKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyKey { get; set; }
    }
}
