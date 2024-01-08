using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemo.Models
{
   
    public class CompanyViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
