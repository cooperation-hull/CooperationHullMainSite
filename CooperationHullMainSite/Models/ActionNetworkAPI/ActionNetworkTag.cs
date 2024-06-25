using CooperationHullMainSite.Models.ActionNetworkAPI.FormData;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace CooperationHullMainSite.Models.ActionNetworkAPI.Tags
{


    public class ActionNetworkTag
    {
        public int total_pages { get; set; }
        public int per_page { get; set; }
        public int page { get; set; }
        public int total_records { get; set; }
        [JsonPropertyName("_links")]
        public Links _links { get; set; }
        [JsonPropertyName("_embedded")]
        public Embedded _embedded { get; set; }
    }


    public class Embedded
    {
        [JsonPropertyName("osdi:tags")]
        public List<OsdiTag> osditags { get; set; }
    }


    public class OsdiTag
    {
        public string href { get; set; }
        public string name { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public List<string> identifiers { get; set; }
        public Links _links { get; set; }
    }

    public class Links
    {
        //public Next next { get; set; }

        [JsonProperty("osdi:tags")]
        public List<OsdiTag> osditags { get; set; }
        public List<Cury> curies { get; set; }
        public Self self { get; set; }

        [JsonProperty("osdi:taggings")]
        public OsdiTaggings osditaggings { get; set; }
    }


}
