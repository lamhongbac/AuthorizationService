using System;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseQASubQuestionReport
    {
        public Guid ID { get; set; }
        public int QAReportID { get; set; }
        public int QuestionID { get; set; }
        public int SubQuestionID { get; set; }
        public string Code { get; set; }
        public bool IsPass { get; set; }
        public bool IsCCP { get; set; }
        public bool IsRisk { get; set; }
        public string Note { get; set; }
        public string Images { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal Point { get; set; }
    }
}
