﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseQAActionPlan
    {
        public BaseQAActionPlan()
        {
            QAActionPlanDetails = new List<BaseQAActionPlanDetail> ();
        }
        public Guid ID { get; set; }
        public int QAReportID { get; set; }
        public int QuestionID { get; set; }
        public int SubQuestionID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<BaseQAActionPlanDetail> QAActionPlanDetails { get; set; }
    }
}