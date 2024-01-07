using Microsoft.Build.Framework;
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
        [Required]
        public string UserName { get; set; } 
        [Required]
        public string Password { get; set; } 
        [Required]
        public bool KeepLogined { get; set; }
    }
}
