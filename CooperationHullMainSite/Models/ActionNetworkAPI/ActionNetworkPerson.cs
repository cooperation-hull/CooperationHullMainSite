﻿using System.Dynamic;
using System.Text.Json.Serialization;
namespace CooperationHullMainSite.Models.ActionNetworkAPI
{

    [Serializable]
    public class ActionNetworkPersonSignupHelper
    {
        public ActionNetworkPerson person {  get; set; }
        public string[] add_tags { get; set; }
        public ActionNetworkPersonSignupHelper() { }

        public ActionNetworkPersonSignupHelper(ActionNetworkPerson data) {
            person = data;
        }

        public ActionNetworkPersonSignupHelper(ActionNetworkPerson data, List<string> tagsToAdd) {
            person = data;
            add_tags = tagsToAdd.ToArray();
        }
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
        public List<ActionNetworkPhone> phone_numbers;
        [JsonInclude]
        public List<ActionNetworkAddress> postal_addresses;
        [JsonInclude]
        public dynamic? custom_fields;
        public DateTime? created_date {  get; set; }
        public DateTime? modified_date { get; set; }

        public ActionNetworkPerson() {

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
        public string? number_type { get; set; }
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
