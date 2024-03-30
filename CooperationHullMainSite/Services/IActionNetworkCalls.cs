namespace CooperationHullMainSite.Services
{
    public interface IActionNetworkCalls
    {
        Task<int> GetNumberSigned(string formName);
        Task<bool> SubmitForm(string formName, object formData);
    }
}
