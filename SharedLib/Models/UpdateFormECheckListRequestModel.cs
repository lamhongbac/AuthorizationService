using SharedLib.BaseObjects.Checklist;
using System;
using System.Collections.Generic;

namespace SharedLib.Models
{
    public class UpdateFormECheckListRequestModel : BaseUpdateRequestModel
    {
        public UpdateFormECheckListRequestModel()
        {
            ID = new Guid();
            FormECheckListDetails = new List<BaseFormECheckListDetail>();
        }
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public List<BaseFormECheckListDetail> FormECheckListDetails { get; set; }
    }
}
