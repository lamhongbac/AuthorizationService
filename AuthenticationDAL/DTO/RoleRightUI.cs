using Dapper.Contrib.Extensions;

namespace AuthenticationDAL.DTO
{
    [Table("RoleRights")]
    public class RoleRightUI : BaseUI
    {
        [Key]
        public int Id { get; set; }
        public int RoleID { get; set; }
        public int AppObjectID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
