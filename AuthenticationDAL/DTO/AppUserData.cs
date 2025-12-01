using System.Collections.Generic;

namespace AuthenticationDAL.DTO
{
    /// <summary>
    /// AppUsers(Companies
    /// </summary>
    public class AppUserData
    {
        public AppUserData()
        {
            AppUser = null;
            Company = new CompanyUI();
            AppRole = new AppRoleUI();
            RoleRights = new List<RoleRightUI>();
        }
        public AppUserUI AppUser { get; set; }
        public CompanyUI Company { get; set; }
        public AppRoleUI AppRole { get; set; }
        public List<RoleRightUI> RoleRights { get; set; }
        public List<UserStoreUI> UserStores { get; set; }
    }
}
