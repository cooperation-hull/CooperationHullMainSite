using CooperationHullMainSite.Models;

namespace CooperationHullMainSite.Services
{
    public interface ISanityCMSCalls
    {
        Task<List<HappeningNextEvent>> GetHomePageEventsList();
        Task<List<PostSummary>> GetBlogPostsList();
        Task<bool> GetBlogPostDetails();
    }
}
