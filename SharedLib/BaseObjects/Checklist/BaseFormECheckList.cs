using System;
using System.Collections.Generic;
using System.Text;

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
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public List<BaseFormECheckListDetail> FormECheckListDetails { get; set; }
    }
}
