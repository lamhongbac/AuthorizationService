using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyApiAuth.Models;

namespace ProtectResourceService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [Authorize]
        [HttpGet(Name = "GetCompanies")]
        [HasPermission("company;read,list")]
        public IActionResult GetCompanies()
        {
            List<Company> companies = new List<Company>()
            { new Company{ ID=1, Name="Kfc"}, new Company{ ID=2,Name="Golden Gate group"},
            new Company{ ID=3,Name="Sasin Group"}
            };
            return Ok(companies);
        }
        [Authorize]
        [HttpPost(Name = "CreateCompany")]
        [HasPermission("company;create")]
        public IActionResult CreateCompany(Company company)
        {

            return Ok(company);
        }
        [Authorize]
        [HttpGet(Name = "GetAbout")]
        [HasPermission("about;read,list")]
        public IActionResult GetAbout()
        {
            string about = "this is this , and this is not this, so we made decision to do this, not to to this";
            return Ok(about);
        }
        [Authorize]
        [HttpPost(Name = "UpdateAbout")]
        [HasPermission("about;update")]
        public IActionResult UpdateAbout(AboutData aboutData)
        {
            string about = "this is this , and this is not this, so we made decision to do this, not to to this";
            return Ok(about);
        }
    }
}
