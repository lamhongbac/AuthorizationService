using AuthenticationDemo.Library;
using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        WeUtils _webUtils;
        IConfiguration _configuration;
        AppConfig _appConfig;
        AccountService _accountService;
        public HomeController(ILogger<HomeController> logger,
            WeUtils webUtils, AccountService accountService,
            IConfiguration configuration)
        {
            _logger = logger;
            _webUtils= webUtils;
            _configuration = configuration;
            _appConfig = configuration.GetSection("AppConfig").Get<AppConfig>();
            _accountService = accountService;

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

            await _accountService.Logout();

            if (_appConfig.IsForceLogin)
            {
                if (!_webUtils.IsLogin())
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
        public IActionResult Privacy()
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
        public IActionResult Logout()
        {
            return View();
        }
        [AllowAnonymous]
        //AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}