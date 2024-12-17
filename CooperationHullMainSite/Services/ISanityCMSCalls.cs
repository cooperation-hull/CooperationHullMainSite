using CooperationHullMainSite.Models;

namespace CooperationHullMainSite.Services
{
    public interface ISanityCMSCalls
    {
        Task<List<HappenginNextEvent>> GetHomePageEventsList();
    }
}
