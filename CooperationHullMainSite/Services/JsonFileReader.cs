using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CooperationHullMainSite.Services
{
    public class JsonFileReader : IJsonFileReader
    {

        private IWebHostEnvironment _hostingEnvironment;

        public JsonFileReader(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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
                ex.ToString();
            }

            return jsonString;
        }

        private string rootFilePath()
        {
            return _hostingEnvironment.WebRootPath; 
        }

    }

}
