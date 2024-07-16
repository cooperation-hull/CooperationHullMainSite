using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.ActionNetworkAPI;
using CooperationHullMainSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;
using System.Text.Json;
using System.Web;

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
                string temp = await _jsonFileReader.ReadFile("/content/homepageContent.json");

                model = JsonSerializer.Deserialize<HomePageModel>(temp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not deserialize content file, defaults used");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Home_page_signup_form()
        {
            var data = new ActionNetworkPerson();

            var errorList = new List<string>();

            //Note relying on actionnetworks api to handle sql injection checks etc.
            //TODO - maybe write validation/ input sanitization service, overkill atm but may be useful later?
            if(String.IsNullOrWhiteSpace(Request.Form["PledgeFormSurname"]))
                errorList.Add("Surname");
            else
                data.family_name = HttpUtility.HtmlEncode(Request.Form["PledgeFormSurname"]);


            if (String.IsNullOrWhiteSpace(Request.Form["PledgeFormFirstName"]))
                errorList.Add("First Name");
            else
                data.given_name = HttpUtility.HtmlEncode(Request.Form["PledgeFormFirstName"]);


            if (String.IsNullOrEmpty(Request.Form["PledgeFormMobile"]))
            { errorList.Add("Phone number"); }
            else
            {
                var phone = new ActionNetworkPhone(HttpUtility.HtmlEncode(Request.Form["PledgeFormMobile"]));
                    phone.primary = true;
                    data.phone_numbers = [phone];
            }

            bool liveInHull = false;

            if(Request.Form["LiveInHull"] == "on")
            {
                liveInHull = true;
            }


            bool result = false;

            if(errorList.Count > 0)
            {
                string errorMessage = $"Please complete the following fields {string.Join(", ", errorList)}";
                return Json(new { result = result, error = errorMessage });
            }
            else
            {
                result = await _actionNetworkCalls.SubmitNewPersonRecord(data, liveInHull);

                if (result)
                    return Json(new { result = result, signedByName = data.given_name });
                else
                    return Json(new { result = result, error = "Something went wrong. Please try again later;" });
            }
        }


        [HttpGet]
        [Route("privacy-policy")]
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        [HttpGet]
        [Route("cookie-policy")]
        public IActionResult CookiePolicy()
        {
            return View();
        }

        [HttpGet]
        [Route("how-we-work")]
        public IActionResult HowWeWork()
        {
            return View();
        }

        [HttpGet]
        [Route("the-big-idea")]
        public IActionResult TheBigIdea()
        {
            return View();
        }


        [HttpGet]
        [Route("membership")]
        public IActionResult Membership()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Membership_Post()
        {
            //Memberships form submission handling will go here
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("faqs")]
        public IActionResult FAQs()
        {
            return View();
        }

        [HttpGet]
        [Route("help-to-attend")]
        public IActionResult HelpToAttend()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HelpToAttend_Post()
        {
            //Help To Attend form submission handling will go here
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route("the-pledge")]
        public IActionResult ThePledge()
        {
            return View();
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
