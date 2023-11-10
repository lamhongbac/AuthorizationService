using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseQAConfirmResultReport
    {
        public Guid ID { get; set; }
        public int QAReportID { get; set; }
        public string RMCode { get; set; }
        public DateTime ConfirmedDate { get; set; }
        public string Note { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
