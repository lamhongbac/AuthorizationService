using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("CompanyApplication")]
    public class CompanyApplicationUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int AppID { get; set; }
        public string Description { get; set; }
        public string AppKey { get; set; }
    }
}
