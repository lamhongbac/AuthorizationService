using SharedLib;

namespace AuthenticationDemo.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; } = string.Empty;       
        public string Password { get; set; } = string.Empty;
        public bool KeepLogined { get; set; }
    }
}
