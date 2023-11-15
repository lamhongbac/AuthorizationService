using System.Collections.Generic;

namespace AuthorizationService.BaseObjects
{
    /// <summary>
    /// 1 User se truc thuong 1 cty
    /// 1 User se co 1 role
    /// 1 role tuong ung se co cac quyen la danh object va quyen tren nó
    /// </summary>
    public class BaseAppUser : BaseData
    {
        public BaseAppUser() : base()
        {
            Company = new BaseCompany();
            Role = new BaseAppRole();
        }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Pwd { get; set; }
        public string PwdKey { get; set; }
        public int CompanyAppID { get; set; }
        public int AppID { get; set; }
        public BaseCompany Company { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleID { get; set; }
        public int? ManagerID { get; set; }
        public bool IsManager { get; set; }
        public string Department { get; set; }
        public BaseAppRole Role { get; set; }
        public List<BaseUserStore> BaseUserStores { get; set; }
    }
}
