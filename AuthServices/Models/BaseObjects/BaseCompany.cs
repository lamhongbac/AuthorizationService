namespace AuthorizationService.BaseObjects
{
    public class BaseCompany:BaseData
    {
        public int ID { get; set; }
        public string? RegKey { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CompanyKey { get; set; }
    }
}
