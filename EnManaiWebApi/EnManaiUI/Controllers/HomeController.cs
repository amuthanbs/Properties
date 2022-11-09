using EnManaiUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TokenBased.Model;

namespace EnManaiUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly JwtSettings jwtSettings;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, JwtSettings jwtSettings)
        {
            _logger = logger;
            this.jwtSettings = jwtSettings;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult LoggedSearch(string city)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public IActionResult Login(LoginRequest loginRequest)
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult LandingPage(int id)
        {
            return View();
        }

        public IActionResult Search(string city)
        {
            return View();
        }
        //public IActionResult SearchResult(string city)
        //{
        //    return View();
        //}
    }
}