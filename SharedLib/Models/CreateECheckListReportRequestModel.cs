using SharedLib.BaseObjects.Checklist;
using System;
using System.Collections.Generic;

namespace SharedLib.Models
{
    public class CreateECheckListReportRequestModel : BaseCreateRequestModel
    {
        public CreateECheckListReportRequestModel()
        {
            ID = new Guid();
            FormID = new Guid();
            ECheckListDetailReports = new List<BaseECheckListDetailReport>();
        }
        public Guid ID { get; set; }
        public Guid FormID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int UserID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public List<BaseECheckListDetailReport> ECheckListDetailReports { get; set; }
    }
}
