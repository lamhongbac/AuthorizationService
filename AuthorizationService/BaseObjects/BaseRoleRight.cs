namespace AuthorizationService.BaseObjects
{
    public class BaseRoleRight:BaseData
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public int AppObjectID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}
