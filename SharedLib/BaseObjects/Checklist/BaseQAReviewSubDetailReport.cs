using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseQAReviewSubDetailReport
    {
        public Guid ID { get; set; }
        public Guid QASubReportID { get; set; }
        public Guid ReviewReportID { get; set; }
        public bool IsAvailable { get; set; }
        public string Code { get; set; }
        public bool IsPass { get; set; }
        public bool IsCCP { get; set; }
        public bool IsRisk { get; set; }
        public string Note { get; set; }
        public string Images { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal Point { get; set; }
        public int QAQuestionID { get; set; }
        public string CheckStatus { get; set; }
    }
}
