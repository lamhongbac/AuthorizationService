namespace AuthorizationService.BaseObjects
{
    public class BaseUserRole:BaseData
    {
        public int ID { get; set; }
        public int CompanyAppID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
