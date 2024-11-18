namespace CooperationHullMainSite.Models.ConfigSections
{
    public class SanityCMSConfig
    {
        public required string ProjectID { get; set; }
        public required string DatasetName { get; set; }
        public string? APIKey { get; set; }
    }
}
