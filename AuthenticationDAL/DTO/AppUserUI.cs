﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("AppUsers")]
    public class AppUserUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Pwd { get; set; }
        public int CompanyAppID { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleID { get; set; }
    }
}