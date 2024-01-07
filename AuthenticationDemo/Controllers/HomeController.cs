using AuthenticationDemo.Models;
using AuthenticationDemo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AuthenticationDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        WeUtils _webUtils;
        public HomeController(ILogger<HomeController> logger, WeUtils webUtils)
        {
            _logger = logger;
            _webUtils= webUtils;
        }

        public IActionResult Index()
        {
            
            if (!_webUtils.IsLogin())
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Logout()
        {
            return View();
        }
        //AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}