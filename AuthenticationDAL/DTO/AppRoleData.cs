using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticationDAL.DTO
{
    public class AppRoleData
    {
        public AppRoleData()
        {
            AppRoleUI = new AppRoleUI();
            RoleRightUIs = new List<RoleRightUI>();
        }
        public AppRoleUI AppRoleUI { get; set; }
        public List<RoleRightUI> RoleRightUIs { get; set; }
    }
}
