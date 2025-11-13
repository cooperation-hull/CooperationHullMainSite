using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.ActionNetworkAPI;
using CooperationHullMainSite.Models.SanityCMS;
using CooperationHullMainSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Web;
using X.PagedList.Extensions;

namespace CooperationHullMainSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActionNetworkCalls _actionNetworkCalls;
        private readonly ISanityCMSCalls _sanityCMSCalls;
        public HomeController(ILogger<HomeController> logger,
                                IActionNetworkCalls actionNetworkCalls,
                                ISanityCMSCalls CMSCalls)
        {
            _logger = logger;
            _actionNetworkCalls = actionNetworkCalls;
            _sanityCMSCalls = CMSCalls;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HomePageModel model = new HomePageModel();
            model.eventList = await _sanityCMSCalls.GetHomePageEventsList();

            return View(model);
        }



        [HttpGet]
        [Route("blog")]
        public async Task<IActionResult> OurBlog(int? page)
        {
            BlogSummaryModel model = new BlogSummaryModel();

            model.TopPost = await _sanityCMSCalls.GetLatestBlogPostSummary();

            int pageNumber = page ?? 1;
            var postsList = await _sanityCMSCalls.GetAllBlogPostsList();
            var onePage = postsList.ToPagedList(pageNumber, 3);

            model.PostsList = onePage;

            return View(model);
        }


        [HttpGet]
        [Route("blog/{slug}")]
        public async Task<IActionResult> BlogPost(string slug)
        {
            //TODO - add 404 handling for invalid slugs
            //TODO - add in tags to post and use them in og tags

            BlogPostContent model = new BlogPostContent();

            model = await _sanityCMSCalls.GetBlogPostDetails(slug);

            if (model == null || model.contentHtml == null)
            {
                return RedirectToAction("OurBlog");
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
            if (String.IsNullOrWhiteSpace(Request.Form["PledgeFormSurname"]))
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

            if (Request.Form["LiveInHull"] == "yes")
            {
                liveInHull = true;
            }
            else if (Request.Form["LiveInHull"] == "no")
            {
                liveInHull = false;
            }
            else
            {
                errorList.Add("Do you live in Hull?");
            }

            bool result = false;

            if (errorList.Count > 0)
            {
                string errorMessage = $"Please complete: {string.Join(", ", errorList)}";
                return Json(new { result = result, error = errorMessage });
            }
            else
            {
                result = await _actionNetworkCalls.SubmitNewPersonRecord(data, liveInHull);

                if (result)
                    return Json(new { result = result, signedByName = data.given_name });
                else
                    return Json(new { result = result, error = "Sorry, that didn't work. Please try again later" });
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
        [Route("in-practice")]
        public IActionResult InPractice()
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
        [Route("principles-and-values")]
        public IActionResult PrinciplesAndValues()
        {
            return View();
        }


        [HttpGet]
        [Route("events")]
        public async Task<IActionResult> Events()
        {

            EventPageModel model = await _sanityCMSCalls.GetAllEventsPageData();

            if (model == null || model.events.Count == 0)
            {
                return RedirectToAction("Index");
            }

            return View(model);
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
