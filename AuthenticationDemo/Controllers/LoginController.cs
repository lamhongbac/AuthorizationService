using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using AuthServices;
using AuthServices.Models;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// neu da login thi quay ve home
        /// </summary>
        /// <returns></returns>
        public IActionResult Login( )
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LoginViewModel model = new LoginViewModel();
                return View(model);
            }
        }
        /// <summary>
        /// @Html.ActionLink("Login", "Login", "Account", 
        /// new {@returnUrl = Url.Action(ViewContext.RouteData.Values["action"].ToString(), ViewContext.RouteData.Values["controller"].ToString())})
        /// Neu view not valid thi quay ve view de nhap lai
        /// Neu login thanh cong thi quay ve home
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model  )
        {
            if (!ModelState.IsValid)
            {
                return View(model);
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
