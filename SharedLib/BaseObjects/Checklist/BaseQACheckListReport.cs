﻿using SharedLib.BaseObjects.Orgchart;
using System;
using System.Collections.Generic;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseQACheckListReport
    {
        public BaseQACheckListReport()
        {
            QASubQuestionReports = new List<BaseQASubQuestionReport>();
            Outlet = new BaseOutlet();
        }
        public int ID { get; set; }
        public int FormID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string QAOfficerName { get; set; }
        public string QAOfficerID { get; set; }
        public int PassPoint { get; set; }
        public int StoreID { get; set; }
        public string RestaurantStaffs { get; set; }
        public decimal SuccessPercent { get; set; }
        public decimal PointBSC { get; set; }
        public string ResultStatus { get; set; }
        public DateTime CheckDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string SummaryNotes { get; set; }
        public string ViolationStatus { get; set; }
        public BaseOutlet Outlet { get; set; }
        public List<BaseQASubQuestionReport> QASubQuestionReports { get; set; }
    }
}
