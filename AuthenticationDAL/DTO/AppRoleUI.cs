using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    [Table("AppRoles")]
    public class AppRoleUI:BaseUI
    {
        [Key]
        public int ID { get; set; }
        public int CompanyAppID { get; set; } //Or AppID
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// thuoc tinh nay de xac dinh role nay co can danh sach nha hang hay khong
        /// neu is store admin = false thi co nghia kg can
        /// cac role nhu QA manager, Hoac tuong tu thi kg can ds nha hang
        /// cac role QA Officer thi cung co the (neu DN nho)
        /// cac role RM thi can co DS nha hang (bat buoc)
        /// </summary>
        public bool IsStoreAdmin { get; set; }
    }
}
