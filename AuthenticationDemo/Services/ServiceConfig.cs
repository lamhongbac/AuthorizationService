namespace AuthenticationDemo.Services
{
    public class ServiceConfig
    {
        public string AuthServiceBaseAddress { get; set; }
        public string ProtectedServiceBaseAddress { get; set; }

        public string GetCompanies { get; set; }
        public string CreateCompany { get; set; }
        public string GetAbout { get; set; }
        public string UpdateAbout { get; set; }


        public string Login { get; set; }
        public string RenewToken { get; set; }
    }
}
