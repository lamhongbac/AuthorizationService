using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("mssMobileTracking")]
    public class MobileTrackingUI : BaseUI
    {
        [ExplicitKey]
        public Guid ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? Token { get; set; }
        public bool? IsIn { get; set; }
        public string? AppID { get; set; }
    }
}
