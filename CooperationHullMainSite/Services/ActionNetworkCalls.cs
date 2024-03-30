using CooperationHullMainSite.Controllers;

namespace CooperationHullMainSite.Services
{
    public class ActionNetworkCalls : IActionNetworkCalls
    {

        private readonly ILogger<ActionNetworkCalls> _logger;
        private readonly IConfiguration _configuration;

        private string APIKey { get; set; }
        private string baseURL { get; set; }
        public ActionNetworkCalls(IConfiguration configuration,
                                    ILogger<ActionNetworkCalls> logger)
        {
            _configuration = configuration;
            _logger = logger;

            APIKey = _configuration["ActionNetworkConfig:APIKey"];
            baseURL = _configuration["ActionNetworkConfig:baseURL"];
        }

        public async Task<int> GetNumberSigned(string formName)
        {

            var result = MakeAPICall("", new object());

            return 500;
        }
        public async Task<bool> SubmitForm(string formName, object formData)
        {
            throw new NotImplementedException();
        }


        private  async Task<object> MakeAPICall(string endpoint, object dataModel)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Add("OSDI-API-Token", APIKey);

                HttpResponseMessage response = await client.GetAsync(endpoint);

                var data = "";

               if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                 data = await response.Content.ReadAsStringAsync();

                    //Do stuff to get data into a useful format
                }
               else {
                    _logger.LogError($"API Call to {endpoint} failed: {response.StatusCode.ToString()}");
                }

            }

            return new object();
        }


    }
}
