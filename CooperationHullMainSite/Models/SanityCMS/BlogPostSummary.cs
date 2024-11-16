namespace CooperationHullMainSite.Models.SanityCMS
{
    public class BlogPostSummary
    {
        public string title { get; set; }
        public DateTime date { get; set; }
        public string slug { get; set; }
        public string author { get; set; }
        public string[] tags { get; set; }
        public string image { get; set; }
        public string summary { get; set; }
    }
}
