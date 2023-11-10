using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.Models
{
    public class JwtData
    {
        public string AccessToken { get; set; } //Jwt
        public string RefreshToken { get; set; }
        public string Jwt { get; set; }
    }
}
