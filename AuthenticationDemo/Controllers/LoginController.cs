using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using AuthServices;
using AuthServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers
{
    public class LoginController : Controller
    {
        AccountService accountService;
        public LoginController(AccountService accountService)
        {
            this.accountService = accountService;
        }
        public IActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            bool isLogin =await accountService.Login(model);

            if (isLogin)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
