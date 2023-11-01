using Dapper.Contrib.Extensions;

namespace AuthenticationDAL.DTO
{
    [Table("Applications")]
    public class ApplicationUI : BaseUI
    {
        [Key]
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
