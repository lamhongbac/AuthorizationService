using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("LogLogin")]
    public class LogLoginUI
    {
        [ExplicitKey]
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? EmailAddress { get; set; }
        public int AppID { get; set; }
        public int CompanyID { get; set; }
        public int? ManagerID { get; set; }
        public string? ManagerEmail { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
    }
}
