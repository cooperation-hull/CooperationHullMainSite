using CooperationHullMainSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CooperationHullMainSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult WhoWeAre()
        {
            return View();
        }

        public IActionResult TheBigIdea()
        {
            return View();
        }

        public IActionResult AroundTheWorld()
        {
            return View();
        }

        public IActionResult FAQs()
        {
            return View();
        }

        public IActionResult Donate()
        {
            return View();
        }

        public IActionResult HelpToAttend()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
