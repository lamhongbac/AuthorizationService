using System;

namespace SharedLib.BaseObjects.Checklist
{

    public class BaseECheckListDetailReport
    {
        public Guid ID { get; set; }
        public Guid ParentID { get; set; }
        public Guid FormDetailID { get; set; }
        public bool IsDone { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
