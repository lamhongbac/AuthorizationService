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
        WeUtils _webUtils;
        public LoginController(AccountService accountService, WeUtils webUtils)
        {
            this.accountService = accountService;
            _webUtils = webUtils;
        }
        /// <summary>
        /// neu da login thi quay ve home
        /// </summary>
        /// <returns></returns>
        public IActionResult Login(string? ReturnUrl = null )
        {
           
            if (_webUtils.IsLogin())
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ReturnUrl"] = ReturnUrl;
                LoginViewModel model = new LoginViewModel();
                return View(model);
            }
        }
        /// <summary>
        /// @Html.ActionLink("Login", "Login", "Account", 
        /// new {@returnUrl = Url.Action(ViewContext.RouteData.Values["action"].ToString(), ViewContext.RouteData.Values["controller"].ToString())})
        /// Neu view not valid thi quay ve view de nhap lai
        /// Neu login thanh cong thi quay ve home
        /// https://dotnettutorials.net/lesson/redirect-to-returnurl-after-login-in-asp-net-core/
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]        
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            bool isLogin =await accountService.Login(model);

            if (isLogin)
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    // Redirect to default page
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View();
            }
        }
    }
}
