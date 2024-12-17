using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.SanityCMS;

namespace CooperationHullMainSite.Services
{
    public interface ISanityCMSCalls
    {
        Task<List<HappeningNextEvent>> GetHomePageEventsList();
        Task<List<PostSummary>> GetBlogPostsList(int startIndex, int quantity);
        Task<BlogPostContent> GetBlogPostDetails(string slug);
        Task<PostSummary> GetLatestBlogPostSummary();

    }
}
