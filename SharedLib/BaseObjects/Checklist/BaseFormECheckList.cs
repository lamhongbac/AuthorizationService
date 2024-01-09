using System;
using System.Collections.Generic;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseFormECheckList
    {
        public BaseFormECheckList()
        {
            FormECheckListDetails = new List<BaseFormECheckListDetail>();
        }
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string RiskText { get; set; }
        public string CCPText { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Routine { get; set; }
        public string CheckListType { get; set; }
        public decimal PassPoint { get; set; }
        public List<BaseFormECheckListDetail> FormECheckListDetails { get; set; }
    }
}
