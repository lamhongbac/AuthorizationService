﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("Applications")]
    public class ApplicationUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
    }
}