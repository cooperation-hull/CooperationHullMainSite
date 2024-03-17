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


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string temp = await _jsonFileReader.ReadFile("\\content\\homepageContent.json");

            //TODO error handling!!!!
           HomePageModel model = JsonSerializer.Deserialize<HomePageModel>(temp);
 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index_post()
        {
            //Homepage form handling will go here.
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("PrivacyPolicy")]
        public async Task<IActionResult> PrivacyPolicy()
        {
            return View();
        }

        [HttpGet]
        [Route("CookiePolicy")]
        public async Task<IActionResult> CookiePolicy()
        {
            return View();
        }

        [HttpGet]
        [Route("ContactUs")]
        public async Task<IActionResult> ContactUs()
        {
            return View();
        }


        [HttpGet]
        [Route("WhoWeAre")]
        public async Task<IActionResult> WhoWeAre()
        {
            return View();
        }

        [HttpGet]
        [Route("TheBigIdea")]
        public async Task<IActionResult> TheBigIdea()
        {
            return View();
        }


        [HttpGet]
        [Route("ToolsOfTheTrade")]
        public async Task<IActionResult> ToolsOfTheTrade()
        {
            return View();
        }


        [HttpGet]
        [Route("AroundTheWorld")]
        public async Task<IActionResult> AroundTheWorld()
        {
            return View();
        }


        [HttpGet]
        [Route("Membership")]
        public async Task<IActionResult> Membership()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Membership_Post()
        {
            //Memberships form submission handling will go here
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("FAQs")]
        public async Task<IActionResult> FAQs()
        {
            return View();
        }

        [HttpGet]
        [Route("HelpToAttend")]
        public async Task<IActionResult> HelpToAttend()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> HelpToAttend_Post()
        {
            //Help To Attend form submission handling will go here
            throw new NotImplementedException();
        }


        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
