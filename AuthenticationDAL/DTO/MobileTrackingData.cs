using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    public class MobileTrackingData
    {
        public MobileTrackingData()
        {
            MobileTracking = new MobileTrackingUI();
            MobileTrackingDetails = new List<MobileTrackingDetailUI>();
        }
        public MobileTrackingUI MobileTracking { get; set; }
        public List<MobileTrackingDetailUI> MobileTrackingDetails { get; set; }
    }
}
