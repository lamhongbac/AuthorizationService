using System;

namespace AuthServices.Models
{
    public class ECheckListReportRequestModel : RequestModel
    {
        public ECheckListReportRequestModel()
        {
            FormID = Guid.NewGuid();
        }
        public int? UserID { get; set; }
        public Guid? FormID { get; set; }
    }
}
