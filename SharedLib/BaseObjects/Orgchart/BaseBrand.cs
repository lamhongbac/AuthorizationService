using System;

namespace SharedLib.BaseObjects.Orgchart
{
    public class BaseBrand
    {
        public BaseBrand()
        {

        }
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slogan { get; set; }
        public string Icon { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string About { get; set; }
        public bool IsDeleted { get; set; }
        public string Images { get; set; }
        public string ManagerID { get; set; }
        public int BusinessCateID { get; set; }
    }
}
