using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseStoreQAQuestion
    {
        public BaseStoreQAQuestion()
        {
            BaseStoreSubQAQuestions = new List<BaseStoreSubQAQuestion>();
        }
        public Guid ID { get; set; }
        public int StoreID { get; set; }
        public int QuestionID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string CCPText { get; set; }
        public int RiskText { get; set; }
        public List<BaseStoreSubQAQuestion> BaseStoreSubQAQuestions { get; set; }
    }
}
