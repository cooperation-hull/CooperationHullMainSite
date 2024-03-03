namespace CooperationHullMainSite.Services
{
    public interface IJsonFileReader
    {
        Task<string> ReadFile(string filePath);

    }
}
