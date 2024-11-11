namespace CooperationHullMainSite.Models.ConfigSections
{
    public class ActionNetworkConfig
    {
        public ActionNetworkConfig() { }

        public const string SectionName = "ActionNetworkConfig";
        public string baseURL { get; set; }
        public string APIKey { get; set; }

        public List<FormData> FormList { get; set; }

        public List<string> SignupFormDefaultTags { get; set; }

    }


    public class FormData
    {
        public FormData() { }
        public string FormName { get; set; }
        public string FormGUID { get; set; }
        public string FormID { get; set; }
    }
}
