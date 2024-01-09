using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthenticationDemo.Controllers
{
    public class CompanyController : Controller
    {
        CompanyViewModelHelper _viewModelHelper;
        public CompanyController(CompanyViewModelHelper viewModelHelper)
        {
            _viewModelHelper= viewModelHelper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       
        public async Task<IActionResult> Index()
        {
            List<CompanyViewModel> companies = new List<CompanyViewModel>();
            _viewModelHelper.BearToken = $"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJiZGVhZGQwZC02OTE1LTQwZjAtOGQyZC0xNTc2OWU2NWQxZDgiLCJVc2VyTmFtZSI6ImFkbWluIiwic3ViIjoiYWRtaW4iLCJVc2VySUQiOiI3IiwiRnVsbE5hbWUiOiJQaOG6oW0gSG_DoG5nIMSQ4bqhdCIsIkFwcElEIjoiMyIsIkNvbXBhbnlJRCI6IjMiLCJSb2xlcyI6InFhbWFuYWdlciIsIk9iamVjdFJpZ2h0cyI6IntcImFwcHJvbGVcIjpbXCJyZWFkXCIsXCJjcmVhdGVcIixcInVwZGF0ZVwiLFwiZGVsZXRlXCJdLFwiYXBwdXNlclwiOltcInJlYWRcIixcImNyZWF0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDAxXCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDAyXCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDA0XCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDAzXCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDA1XCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImVjaGVja2xpc3RfZm9ybTAwMDA2XCI6W1wicmVhZFwiLFwiY3JlYXRlXCIsXCJ1cGRhdGVcIixcImRlbGV0ZVwiXSxcImNvbXBhbnlcIjpbXCJyZWFkXCIsXCJjcmVhdGVcIl19IiwibmJmIjoxNzA0NzcxMTk5LCJleHAiOjE3MDQ3NzE3OTksImlhdCI6MTcwNDc3MTE5OSwiaXNzIjoibXNhLmNvbS52biIsImF1ZCI6Im1zYS5jb20udm4ifQ.VMmhCcsMNgJl6jkTOzdn6SlNJET9hz2bWG2q3VGs1HU";
            try
            {
                CompaniesViewModel viewModel = await _viewModelHelper.GetCompanies();
                if (viewModel != null && viewModel.Errors.Count==0)
                {
                    companies = viewModel.Companies;
                   
                }
                else
                {
                    foreach (var item in viewModel.Errors)
                    {
                        string field=item.Key;
                        List<string> Messages = item.Value;
                        foreach (var mess in Messages)
                        {
                            ModelState.AddModelError(field,mess);
                        }
                    }
                   
                }
                
            }catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(companies);
        }
    }
}
