using SharedLib;
using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices
{
    public class LoginModel:BaseAccountModel
    {
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = AppUserType.Email.ToString();
        public string Password { get; set; } = string.Empty;
        public bool KeepLogined { get; set; }



    }
}
