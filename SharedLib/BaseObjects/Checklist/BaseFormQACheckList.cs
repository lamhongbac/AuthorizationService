using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseFormQACheckList
    {
        public BaseFormQACheckList()
        {
            FormQACheckListDetails = new List<BaseFormQACheckListDetail>();
        }
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int StoreID { get; set; }
        public decimal PassPoint { get; set; }
        public List<BaseFormQACheckListDetail> FormQACheckListDetails { get; set; }
    }
}
