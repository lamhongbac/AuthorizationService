namespace AuthorizationService.Data
{
    public class UserInfo
    {
        public int AppID { get; set; }
        public int CompanyID { get; set; }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Roles { get; set; }
        //public Dictionary<string, object> CustomsClaims { get; set; }
    }
}
