﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseFormQACheckListDetail
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsAvailable { get; set; }
        public int SubQuestionID { get; set; }
        public decimal Point { get; set; }
    }
}
