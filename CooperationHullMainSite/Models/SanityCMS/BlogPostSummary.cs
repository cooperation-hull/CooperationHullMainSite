using Sanity.Linq.CommonTypes;

namespace CooperationHullMainSite.Models.SanityCMS
{
    public class BlogPostSummary
    {
        public string title { get; set; }
        public DateTime date { get; set; }
        public SanitySlug slug { get; set; }
        public string author { get; set; }
        public string[] tags { get; set; }
        public SanityImage image { get; set; }
        public string imageAltText { get; set; }
        public string summary { get; set; }
    }
}
