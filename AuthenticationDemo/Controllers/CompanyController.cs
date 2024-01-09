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
