namespace AuthorizationService.BaseObjects
{
    public class BaseUserStore : BaseData
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int StoreID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyAppID { get; set; }
    }
}
