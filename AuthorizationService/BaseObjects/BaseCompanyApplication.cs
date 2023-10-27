namespace AuthorizationService.BaseObjects
{
    public class BaseCompanyApplication:BaseData
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int AppID { get; set; }
        public string Description { get; set; }
        public string AppKey { get; set; }
    }
}
