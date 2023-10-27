using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("UserStores")]
    public class UserStoreUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int StoreID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyAppID { get; set; }
    }
}
