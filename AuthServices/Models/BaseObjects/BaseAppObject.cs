namespace AuthorizationService.BaseObjects
{
    public class BaseAppObject:BaseData
    {
        public int ID { get; set; }
        public int AppID { get; set; }
        public string MainFunction { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
