using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("mssMobileTrackingDetail")]
    public class MobileTrackingDetailUI : BaseUI
    {
        [ExplicitKey]
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string? AppID { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public string? Description { get; set; }
        public string? DeviceName { get; set; }
        public string? DeviceHid { get; set; }
        public string? Location { get; set; }
        public string? Platform { get; set; }
        public string? IP { get; set; }
        public string? TrackingType { get; set; }
        public string? Token { get; set; }
    }
}
