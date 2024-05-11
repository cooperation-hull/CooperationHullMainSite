using System.Text.Json.Serialization;

namespace CooperationHullMainSite.Models.ActionNetworkAPI
{

    public class ActionNetworkCustomFieldCollection
    {
        [JsonPropertyName("action_network:custom_fields")]
        public List<ActionNetworkCustomField> CustomFields { get; set; }
    }


    public class ActionNetworkCustomField
    {
        public string name { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public string? notes { get; set; }
        public int numeric_id { get; set; }
    }
}
