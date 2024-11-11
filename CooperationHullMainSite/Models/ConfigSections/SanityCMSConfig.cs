namespace CooperationHullMainSite.Models.ConfigSections
{
    public class SanityCMSConfig
    {
        public required string ProjectId { get; set; }
        public required string DatasetName { get; set; }
        public string? APIKey { get; set; }
    }
}
