using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.BaseObjects.Orgchart
{
    public class BaseOutletGroup
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; } = null!;
        public bool? IsDeleted { get; set; }
        public int? BranchId { get; set; }
    }
}
