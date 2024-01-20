using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices.Models
{
    public class LogOutModel:BaseAccountModel
    {
        public string UserID { get; set; } //kg phai userName
    }
}
