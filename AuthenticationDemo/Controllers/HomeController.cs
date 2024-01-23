using AuthenticationDemo.Library;
using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using StudyApiAuth.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       
        IConfiguration _configuration;
        AppConfig _appConfig;
        AccountService _accountService;
        MSASignInManager _msaSignInManager;
        public HomeController(ILogger<HomeController> logger,
           AccountService accountService, MSASignInManager msaSignInManager,
            IConfiguration configuration)
        {
            _logger = logger;
         
            _configuration = configuration;
            _appConfig = configuration.GetSection("AppConfig").Get<AppConfig>();
            _accountService = accountService;
            _msaSignInManager = msaSignInManager;

        }
        /// <summary>
        /// UCase1
        /// Bat buoc phai login truoc khi vao hethong
        /// Thuat giai nhu sau: neu chua login thi redirec to login view
        /// neu da login thi display home page
        /// https://dotnettutorials.net/lesson/redirect-to-returnurl-after-login-in-asp-net-core/
        /// 
        /// UCase2:
        /// Khong bat buoc phai login, chi login khi vao cac action can authorize
        /// su dung strReturnUrl
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //force logout

           // await _accountService.Logout();

            if (_appConfig.IsForceLogin)
            {
                if (!_msaSignInManager.IsSignedIn())
                {
                    string @returnUrl = Url.Action("Index", "Home");
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        [Authorize]
        [HasPermission("about;read,list")]
        public async Task<IActionResult> Privacy()
        {
         
            return View();
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _msaSignInManager.SignOutAsync();

            return RedirectToAction("Index");
           
        }
        [AllowAnonymous]
        //AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}