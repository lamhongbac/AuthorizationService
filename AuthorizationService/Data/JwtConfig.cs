namespace AuthorizationService.Data
{
    public class JwtConfig
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int RefExpireMinutes { get; set;}
        public int ExpiredSeconds { get; set; }
    }
}
