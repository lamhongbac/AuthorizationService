using System.Collections.Generic;

namespace AuthenticationDAL.DTO
{
    public class AppUserData
    {
        public AppUserData()
        {
            AppUser = new AppUserUI();
            Company = new CompanyUI();
            AppRole = new AppRoleUI();
            RoleRights = new List<RoleRightUI>();
        }
        public AppUserUI AppUser { get; set; }
        public CompanyUI Company { get; set; }
        public AppRoleUI AppRole { get; set; }
        public List<RoleRightUI> RoleRights { get; set; }
    }
}
