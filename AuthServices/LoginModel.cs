using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices
{
    public class LoginModel
    {
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
