using CooperationHullMainSite.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CooperationHullMainSite.Services
{
    public class JsonFileReader : IJsonFileReader
    {

        private IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<JsonFileReader> _logger;

        public JsonFileReader(IWebHostEnvironment hostingEnvironment,
                              ILogger<JsonFileReader> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }


        public async Task<string> ReadFile(string filePath)
        {
            string jsonString = "";

            var fullPath = rootFilePath() + filePath;

            try
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    jsonString = await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading json content file");
            }

            return jsonString;
        }

        private string rootFilePath()
        {
            return _hostingEnvironment.WebRootPath; 
        }

    }

}
