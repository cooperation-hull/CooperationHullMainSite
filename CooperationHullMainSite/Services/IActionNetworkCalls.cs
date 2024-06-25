using CooperationHullMainSite.Models.ActionNetworkAPI;
using CooperationHullMainSite.Models.ActionNetworkAPI.Tags;

namespace CooperationHullMainSite.Services
{
    public interface IActionNetworkCalls
    {
        Task<int> GetNumberSigned(string formName);
        Task<bool> SubmitForm(string formName, ActionNetworkPerson formData);
        Task<bool> SubmitNewPersonRecord(ActionNetworkPerson personData);
        Task<List<OsdiTag>> GetListOfTags();
    }
}
