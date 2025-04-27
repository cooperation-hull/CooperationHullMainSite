using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.SanityCMS;

namespace CooperationHullMainSite.Services
{
    public interface ISanityCMSCalls
    {
        Task<List<EventItem>> GetHomePageEventsList();
        Task<BlogPostContent> GetBlogPostDetails(string slug);
        Task<PostSummary> GetLatestBlogPostSummary();
        Task<List<PostSummary>> GetAllBlogPostsList();
        Task<EventPageModel> GetAllEventsPageData();
    }
}
