namespace CooperationHullMainSite.Models
{
    public class BlogSummaryModel
    {

        public List<string> tags { get; set; } = new List<string>();

        public List<PostSummary> PostsList { get; set; } = new List<PostSummary>();
        public PostSummary TopPost { get; set; }

       public BlogSummaryModel() { }

    }


    public class PostSummary
    {
        public string Title { get; set; }
        public DateTime date { get; set; }
        public string slug { get; set; }
        public string author { get; set; }
        public string[] tags { get; set; }
        public string summaryImageUrl { get; set; }
        public string imageAltText { get; set; }
        public string summaryText { get; set; }
    }
}
