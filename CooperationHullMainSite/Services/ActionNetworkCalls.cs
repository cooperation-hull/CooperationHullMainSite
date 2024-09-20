using CooperationHullMainSite.Models.ActionNetworkAPI;
using CooperationHullMainSite.Models.ActionNetworkAPI.FormData;
using CooperationHullMainSite.Models.ActionNetworkAPI.Tags;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

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
            //Currently not used
            ActionNetworkFormData data = await GetFormData(formName);

            return data.total_submissions;
        }


        public async Task<bool> SubmitNewPersonRecord(ActionNetworkPerson formData, bool hullTag)
        {

            List<OsdiTag> tagsData = await GetListOfTags();

            List<string> defaultTags =  config.SignupFormDefaultTags;

            var tagsToAdd = tagsData.Where(x => defaultTags.Any(y => x.name == y)).ToList();

            var tagnameList = tagsToAdd.Select(x => x.name).ToList();

            if (hullTag)
            {
                tagnameList.Remove("Not Hull");
            }
            else
            {
                tagnameList.Remove("Hull");
                tagnameList.Remove("Call me");
            }

            string endpoint = $"people/";
            var temp = await PostDataAPICall(endpoint, new ActionNetworkPersonSignupHelper(formData, tagnameList));
            return temp;
        }


        public async Task<bool> SubmitForm(string formName, ActionNetworkPerson formData)
        {
            // docs - https://actionnetwork.org/docs/v2/record_submission_helper

            var formInfo = FindFormConfigInfo(formName);

            string endpoint = $"forms/{formInfo.FormGUID}/submissions";

            var temp = await PostDataAPICall(endpoint, new ActionNetworkPersonSignupHelper(formData));

            return temp;
        }


        private FormData FindFormConfigInfo(string formName)
        {
            return config.FormList.FirstOrDefault(x => x.FormName == formName);
        }

        private async Task<ActionNetworkFormData> GetFormData(string formName)
        {
            var formInfo = FindFormConfigInfo(formName);

            string endpoint = $"forms/{formInfo.FormGUID}";
            var result = await GetDataAPICall(endpoint, new object());
            var item = JsonSerializer.Deserialize<ActionNetworkFormData>(result);
            return item;
        }

        private  async Task<string> GetDataAPICall(string endpoint, object dataModel)
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


        private async Task<bool> PostDataAPICall(string endpoint, object dataModel)
        {
            bool result = false;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);

            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            //TODO - object serialization - ignore empty lists (steal https://stackoverflow.com/questions/71409542/how-to-ignore-empty-list-when-serializing-to-json) bottom answer extension
            //sort out date format serilization

            var jsonString = JsonSerializer.Serialize(dataModel, options);

            request.Content = new StringContent(jsonString,
                                                Encoding.UTF8,
                                                "application/json");

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(config.baseURL);
                client.DefaultRequestHeaders.Add("OSDI-API-Token", config.APIKey);

                HttpResponseMessage response = await client.SendAsync(request);
                var data = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = true;
                }
                else
                {
                    _logger.LogError($"API Call to {endpoint} failed: {response.StatusCode.ToString()}");
                }

            }

             return result;
        }

       public async Task<List<OsdiTag>> GetListOfTags()
        {
            string endpoint = $"tags/";
            var data = await GetDataAPICall(endpoint, null);

            ActionNetworkTag result = JsonSerializer.Deserialize<ActionNetworkTag>(data);

            return result._embedded.osditags;

        }


    }
}
