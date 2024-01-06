using SharedLib;

namespace AuthenticationDemo.Models
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            UserName = "admin";
            Password = "123456";
            KeepLogined = true;
        }
        public string UserName { get; set; } = string.Empty;       
        public string Password { get; set; } = string.Empty;
        public bool KeepLogined { get; set; }
    }
}
