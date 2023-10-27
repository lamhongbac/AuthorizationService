using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("AppObjects")]
    public class AppObjectUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public int AppID { get; set; }
        public string MainFunction { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
