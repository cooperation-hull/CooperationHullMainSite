using CooperationHullMainSite.Models.Enums;
using System.Text.Json.Serialization;
namespace CooperationHullMainSite.Models.ActionNetworkAPI
{


    [Serializable]
    public class ActionNetworkPersonSignupHelper
    {
        public ActionNetworkPerson person {  get; set; }
        public ActionNetworkPersonSignupHelper() { }

        public ActionNetworkPersonSignupHelper(ActionNetworkPerson data) {
            person = data;
        }

        //todo add in tags etc at some point if needed
    }


    [Serializable]
    public class ActionNetworkPerson
    {
        [JsonInclude] 
        public string family_name { get; set; }
        [JsonInclude] 
        public string given_name { get; set; }
        [JsonInclude] 
        public List<ActionNetworkEmail> email_addresses;
        [JsonInclude] 
        public List<ActionNetworkPhone> phone_number;
        public List<ActionNetworkAddress> postal_addresses;
        public List<KeyValuePair<string, string>> custom_fields;
        public DateTime? created_date {  get; set; }
        public DateTime? modified_date { get; set; }

        public ActionNetworkPerson() {
            email_addresses = new List<ActionNetworkEmail>();
            phone_number = new List<ActionNetworkPhone>();
            postal_addresses = new List<ActionNetworkAddress>();
            custom_fields = new List<KeyValuePair<string, string>>();
        }

    }

    [Serializable]
    public class ActionNetworkEmail
    {
        public bool? primary { get; set; }
        [JsonInclude] 
        public string address { get; set; }
        public string? status { get; set; }
        public ActionNetworkEmail()
        {
        }

        public ActionNetworkEmail(string email)
        {
            address = email;
        }
    }


    [Serializable]
    public class ActionNetworkPhone
    {
        public bool? primary { get; set; }
        public string? number_type { get; set; } = "mobile";
        public string? number { get; set; }
        public string? status { get; set; }

        public ActionNetworkPhone()
        {
        }

        public ActionNetworkPhone(string phoneNumber)
        {
            number = phoneNumber;
        }
    }

    [Serializable]
    public class ActionNetworkAddress
    {
        public bool? primary { get; set; }
        public List<string> address_lines { get; set; }
        public string? locality { get; set; }
        public string? region { get; set; }
        public string? postal_code { get; set; }
        public string? country { get; set; }
        public string? language { get; set; }

    }

}
