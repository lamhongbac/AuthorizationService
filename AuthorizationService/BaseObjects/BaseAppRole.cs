namespace AuthorizationService.BaseObjects
{
    public class BaseAppRole:BaseData
    {
        public int ID { get; set; }
        public int CompanyAppID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsStoreAdmin { get; set; }
    }
}
