using System;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseQAActionPlanDetail
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ParentID { get; set; }
        public string PlanContent { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Images { get; set; }
        public bool IsDelete { get; set; }
    }
}
