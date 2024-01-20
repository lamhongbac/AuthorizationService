using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Checklist
{
    public class BaseResultRating
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public decimal FromPercent { get; set; }
        public decimal ToPercent { get; set; }
        public decimal PointBSC { get; set; }
        public string ResultStatus { get; set; }
    }
}
