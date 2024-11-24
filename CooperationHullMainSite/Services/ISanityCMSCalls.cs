using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.SanityCMS;

namespace CooperationHullMainSite.Services
{
    public interface ISanityCMSCalls
    {
        Task<List<HappeningNextEvent>> GetHomePageEventsList();
        Task<List<PostSummary>> GetBlogPostsList();
        Task<BlogPostContent> GetBlogPostDetails(string slug);
    }
}
