using AuthServices.Models;
using System.Security.Claims;

namespace StudyApiAuth.Models
{
    public class UserInfo
    {
        public UserInfo()
        {
            ObjectRights = new Dictionary<string, List<string>>();
            Roles = new List<string>();
        }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public List<string> Roles { get; set; }

        //objectName;right1,right2
        public Dictionary<string, List<string>> ObjectRights { get; set; }

        public int AppID { get; set; }
        public int CompanyID { get; set; }
    }
    public class LoginInfo
    {
        public LoginInfo()
        {
            LoginDate = DateTime.Now;
            JwtData = new JwtData();

        }

        public DateTime LoginDate { get; set; }
        //example cho truong hop user data tong quat duoi dang string object

        public JwtData JwtData { get; set; }
    }
}
