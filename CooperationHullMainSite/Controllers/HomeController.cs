using CooperationHullMainSite.Models;
using CooperationHullMainSite.Services;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CooperationHullMainSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJsonFileReader _jsonFileReader;

        public HomeController(ILogger<HomeController> logger,
                                IJsonFileReader jsonFileReader)
        {
            _logger = logger;
            _jsonFileReader = jsonFileReader;
        }

        public async Task<IActionResult> Index()
        {
            string temp = await _jsonFileReader.ReadFile("\\content\\homepageContent.json");

            //TODO error handling!!!!
           HomePageModel model = JsonSerializer.Deserialize<HomePageModel>(temp);
 
            return View(model);
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
