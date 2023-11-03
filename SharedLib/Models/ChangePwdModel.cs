using SharedLib;
using SharedLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLib.Models
{
    public class ChangePwdModel: BaseAccountModel
    {
        public string UserName { get; set; } = string.Empty;
        public string UserType { get; set; } = AppUserType.Email.ToString();
        public string OldPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;

    }
}
