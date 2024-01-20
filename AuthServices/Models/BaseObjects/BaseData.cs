using System;

namespace AuthorizationService.BaseObjects
{
    public class BaseData
    {
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
