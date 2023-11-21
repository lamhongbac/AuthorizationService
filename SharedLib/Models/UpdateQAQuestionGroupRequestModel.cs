using System;

namespace SharedLib.Models
{
    public class UpdateQAQuestionGroupRequestModel : BaseUpdateRequestModel
    {
        public UpdateQAQuestionGroupRequestModel()
        {

        }
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
