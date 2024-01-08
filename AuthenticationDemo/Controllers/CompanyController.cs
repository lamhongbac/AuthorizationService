using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<CompanyViewModel> companies =await _viewModelHelper.GetCompanies();
            return View(companies);
        }
    }
}
