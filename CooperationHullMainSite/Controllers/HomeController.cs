using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.ActionNetworkAPI;
using CooperationHullMainSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;
using System.Text.Json;

namespace CooperationHullMainSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJsonFileReader _jsonFileReader;
        private readonly IActionNetworkCalls _actionNetworkCalls;
        public HomeController(ILogger<HomeController> logger,
                                IJsonFileReader jsonFileReader,
                                IActionNetworkCalls actionNetworkCalls)
        {
            _logger = logger;
            _jsonFileReader = jsonFileReader;
            _actionNetworkCalls = actionNetworkCalls;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomePageModel model = new HomePageModel();
            try
            {
                string temp = await _jsonFileReader.ReadFile("\\content\\homepageContent.json");

                model = JsonSerializer.Deserialize<HomePageModel>(temp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not deserialize content file, defaults used");
            }

            try {
                model.formSignedCounter = await _actionNetworkCalls.GetNumberSigned("support-the-hull-peoples-assembly");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not retreive form counter, defaults used");
                model.formSignedCounter = -1;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Home_page_signup_form()
        {
            //Homepage form handling will go here.

            var data = new ActionNetworkPerson();
            data.family_name = Request.Form["PledgeFormSurname"];
            data.given_name = Request.Form["PledgeFormFirstName"];

            var phone = new ActionNetworkPhone(Request.Form["PledgeFormMobile"]);
          //  phone.primary = true;
          //  phone.number_type = "mobile";
            phone.status = "subscribed";

            data.phone_numbers = [phone];

            //todo 
            // VALIDATION!!!!!
            // general xxs etc protection

            var result =  await _actionNetworkCalls.SubmitForm("test_form", data);

            return Json(new { result = result, signedByName = data.given_name });

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
            _logger.LogError(Activity.Current?.Id ?? HttpContext.TraceIdentifier);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
