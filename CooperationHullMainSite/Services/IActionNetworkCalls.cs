using CooperationHullMainSite.Models.ActionNetworkAPI;

namespace CooperationHullMainSite.Services
{
    public interface IActionNetworkCalls
    {
        Task<int> GetNumberSigned(string formName);
        Task<bool> SubmitForm(string formName, ActionNetworkPerson formData);
        Task<bool> SubmitNewPersonRecord(ActionNetworkPerson personData);
    }
}
