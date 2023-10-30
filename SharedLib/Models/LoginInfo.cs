using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices
{
    /// <summary>
    /// Ket qua Login
    /// </summary>
    public class LoginInfo
    {
        public LoginInfo()
        {
            JwtData = new JwtData();
            LoginDate=DateTime.Now;
        }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime LoginDate { get; set; }
        public string Roles { get; set; }
        public JwtData JwtData { get; set; }
    }
}
