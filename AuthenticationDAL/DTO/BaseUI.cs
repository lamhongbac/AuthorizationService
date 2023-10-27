using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    public class BaseUI
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
