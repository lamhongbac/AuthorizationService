namespace AuthenticationDemo.Services
{

    /// <summary>
    /// base post request
    /// </summary>
    public class BaseRequest
    {
        public string Language { get; set; } //dung de API dich message
        public int AppID { get; set; } // API can phanbiet app nao
        public int CompanyID { get; set; } // Api can biet company nao
    }
}
