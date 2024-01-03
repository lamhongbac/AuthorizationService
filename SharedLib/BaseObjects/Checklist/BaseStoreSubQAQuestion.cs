using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseStoreSubQAQuestion
    {
        public Guid ID { get; set; }
        public int StoreID { get; set; }
        public int QuestionID { get; set; }
        public int SubQuestionID { get; set; }
        public bool IsNoteRequired { get; set; }
        public int Points { get; set; }
        public int Priority { get; set; }
        public decimal Point { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
