using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices.Models
{
    /// <summary>
    /// Lop Mob user info
    /// chua thong tin cua user sau khi login
    /// bo xung thuoc tinh DS UserRight
    /// thay the cho Danh sach role va danh sach quyen hien tai
    /// 
    /// </summary>
    public class MobUserInfo
    {
        public MobUserInfo()
        {
            Roles = new List<string>();
            StoreIDs = new List<int>();
            ObjectRights = new List<ObjectRight>();
        }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }


        #region 2 thuoc tinh sau can bo di, vi no kg the hien dung ban chat phan quyen
        public List<string> Roles { get; set; }
        public List<ObjectRight> ObjectRights { get; set; }
        #endregion

        //Bac new prop: 22 Feb
        public List<UserRight> UserRights { get; set; }

        public int AppID { get; set; }
        public int CompanyID { get; set; }
        public int ManagerID { get; set; }
        public string ManagerEmail { get; set; }
        public List<int> StoreIDs { get; set; }
        public Guid LoginID { get; set; }
        public DateTime LoginDate { get; set; }

        /// <summary>
        /// tra ve danh sach object tong can cu vao User Right
        /// su dung ham nay de hien thi cac function can thiet tren giao dien
        /// </summary>
        /// <returns></returns>
        public  List<ObjectRight> GetObjects()
        {
            return new List<ObjectRight>();
        }

        /// <summary>
        /// tra ve danh sach quyen cua 1 objectID
        /// su dung ham nay dung de check quyen tren giao dien
        /// </summary>
        /// <returns></returns>
        public List<String> GetObjectRight(int objectID)
        {
            return new List<string>();
        }
    }
    
}
