using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemo.Models
{
   
    public class CompanyViewModel: BaseViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
    public class CompaniesViewModel: BaseViewModel
    {
        public CompaniesViewModel()
        {
            
        }
        public List<CompanyViewModel> Companies { get; set; }
    }
}
