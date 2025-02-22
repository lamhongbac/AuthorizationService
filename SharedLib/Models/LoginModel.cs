using SharedLib;
using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using MSASharedLib.Utils;

namespace AuthServices.Models
{

    /// <summary>
    /// para cua ham login
    /// UserName co the la email, Mob, OTP, CardNo...vv
    /// 
    /// </summary>
    public class LoginModel:BaseAccountModel
    {
        public LoginModel()
        {
            KeepLogined = true;
        }
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = AppUserType.Email.ToString();
        public string Password { get; set; } = string.Empty;
        public bool KeepLogined { get; set; }



    }
}
