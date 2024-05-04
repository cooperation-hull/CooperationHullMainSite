using CooperationHullMainSite.Models.ActionNetworkAPI;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CooperationHullMainSite.Services
{
    public class ActionNetworkCalls : IActionNetworkCalls
    {

        private readonly ILogger<ActionNetworkCalls> _logger;
        private ActionNetworkConfig config { get; set; }

        public ActionNetworkCalls(IOptions<ActionNetworkConfig> configuration,
                                    ILogger<ActionNetworkCalls> logger)
        {
            config = configuration.Value;
            _logger = logger;
        }

        public async Task<int> GetNumberSigned(string formName)
        {
            ActionNetworkFormData data = await GetFormData(formName);

            return data.total_submissions;
        }

        public async Task<bool> SubmitForm(string formName, object formData)
        {
            throw new NotImplementedException();
        }


        private async Task<ActionNetworkFormData> GetFormData(string formName)
        {
            var formInfo = config.FormList.Where<FormData>(x => x.FormName == formName).FirstOrDefault();

            string endpoint = $"forms/{formInfo.FormGUID}";
            var result = await MakeAPICall(endpoint, new object());
            var item = JsonSerializer.Deserialize<ActionNetworkFormData>(result);
            return item;
        }


        private  async Task<string> MakeAPICall(string endpoint, object dataModel)
        {
            var data = "";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(config.baseURL);
                client.DefaultRequestHeaders.Add("OSDI-API-Token", config.APIKey);

                HttpResponseMessage response = await client.GetAsync(endpoint);

               if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                 data = await response.Content.ReadAsStringAsync();
                }
               else {
                    _logger.LogError($"API Call to {endpoint} failed: {response.StatusCode.ToString()}");
                }

            }

            return data;
        }


    }
}
