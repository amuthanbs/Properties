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
        private readonly IConfiguration _config;
        string APIUrl = null;

        public HomeController(ILogger<HomeController> logger, JwtSettings jwtSettings, IConfiguration config)
        {
            _logger = logger;
            this.jwtSettings = jwtSettings;
            _config = config;
            APIUrl = _config.GetValue<string>("API");
        }

        public IActionResult Index()
        {
            ViewBag.apiurl = APIUrl;
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

        public IActionResult SMSVerification()
        {
            return View();
        }

        public IActionResult SMSVerificat()
        {
            return View();
        }
        //public IActionResult SearchResult(string city)
        //{
        //    return View();
        //}
    }
}