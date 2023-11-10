using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseQAQuestion
    {
        public BaseQAQuestion()
        {
            QASubQuestions = new List<BaseQASubQuestion>();
        }
        public int ID { get; set;}
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CCPText { get; set; }
        public string RiskText { get; set; }
        public int Priority { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ParentID { get; set; }
        public decimal Point { get; set; }
        public List<BaseQASubQuestion> QASubQuestions { get; set; }
    }
}
