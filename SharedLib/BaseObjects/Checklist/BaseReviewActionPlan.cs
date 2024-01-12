using System;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseReviewActionPlan
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ReviewDate { get; set; }
        public Guid ActionPlanID { get; set; }
        public string Description { get; set; }
    }
}
