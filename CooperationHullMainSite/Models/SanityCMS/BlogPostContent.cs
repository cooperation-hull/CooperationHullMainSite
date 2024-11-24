using Newtonsoft.Json.Linq;
using Olav.Sanity.Client;

namespace CooperationHullMainSite.Models.SanityCMS
{
    public class BlogPostContent
    {
       public string _id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string author { get; set; }
        public JArray content { get; set; }
        public string contentHtml { get; set; }
        public string summary { get; set; }
    }
}
