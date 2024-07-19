using System.Text.Json.Serialization;

namespace CooperationHullMainSite.Models.ActionNetworkAPI.FormData
{
    public class ActionNetworkFormData
    {
        public List<string> identifiers { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public string description { get; set; }
        public string call_to_action { get; set; }
        public int total_submissions { get; set; }

        [JsonPropertyName("action_network:sponsor")]
        public ActionNetworkSponsor action_networksponsor { get; set; }
        public Links _links { get; set; }
        public string title { get; set; }
        public string name { get; set; }
        public string browser_url { get; set; }
        public string origin_system { get; set; }
        public Embedded _embedded { get; set; }

        [JsonPropertyName("action_network:hidden")]
        public bool action_networkhidden { get; set; }
    }


    public class ActionNetworkEmbed
    {
        public string href { get; set; }
    }

    public class ActionNetworkSponsor
    {
        public string title { get; set; }
        public string browser_url { get; set; }
    }

    public class Cury
    {
        public string name { get; set; }
        public string href { get; set; }
        public bool templated { get; set; }
    }

    public class EmailAddress
    {
        public bool primary { get; set; }
        public string address { get; set; }
        public string status { get; set; }
    }

    public class Embedded
    {
        [JsonPropertyName("osdi:creator")]
        public OsdiCreator osdicreator { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }

        [JsonPropertyName("osdi:submissions")]
        public OsdiSubmissions osdisubmissions { get; set; }

        [JsonPropertyName("osdi:creator")]
        public OsdiCreator osdicreator { get; set; }

        [JsonPropertyName("action_network:embed")]
        public ActionNetworkEmbed action_networkembed { get; set; }
        public List<Cury> curies { get; set; }

        [JsonPropertyName("osdi:record_submissions_helper")]
        public OsdiRecordSubmissionsHelper osdirecord_submissions_helper { get; set; }

        [JsonPropertyName("osdi:signatures")]
        public OsdiSignatures osdisignatures { get; set; }

        [JsonPropertyName("osdi:donations")]
        public OsdiDonations osdidonations { get; set; }

        [JsonPropertyName("osdi:taggings")]
        public OsdiTaggings osditaggings { get; set; }

        [JsonPropertyName("osdi:outreaches")]
        public OsdiOutreaches osdioutreaches { get; set; }

        [JsonPropertyName("osdi:attendances")]
        public OsdiAttendances osdiattendances { get; set; }
    }

    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string accuracy { get; set; }
    }

    public class OsdiAttendances
    {
        public string href { get; set; }
    }

    public class OsdiCreator
    {
        public string href { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public List<string> identifiers { get; set; }
        public List<EmailAddress> email_addresses { get; set; }
        public List<PhoneNumber> phone_numbers { get; set; }
        public List<PostalAddress> postal_addresses { get; set; }
        public Links _links { get; set; }
        public dynamic custom_fields { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public List<string> languages_spoken { get; set; }
    }

    public class OsdiDonations
    {
        public string href { get; set; }
    }

    public class OsdiOutreaches
    {
        public string href { get; set; }
    }

    public class OsdiRecordSubmissionsHelper
    {
        public string href { get; set; }
    }

    public class OsdiSignatures
    {
        public string href { get; set; }
    }

    public class OsdiSubmissions
    {
        public string href { get; set; }
    }

    public class OsdiTaggings
    {
        public string href { get; set; }
    }

    public class PhoneNumber
    {
        public bool primary { get; set; }
        public string number { get; set; }
        public string number_type { get; set; }
        public string status { get; set; }
    }

    public class PostalAddress
    {
        public bool primary { get; set; }
        public List<string> address_lines { get; set; }
        public string locality { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public Location location { get; set; }
    }

}
