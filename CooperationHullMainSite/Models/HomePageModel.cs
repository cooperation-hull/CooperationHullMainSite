namespace CooperationHullMainSite.Models
{

    public class HomePageModel
    {
        public string title { get; set; } = "";
        public List<HappenginNextEvent> eventList { get; set; } = new List<HappenginNextEvent>();
        public HomePageModel() { }
    }

    public class HappenginNextEvent
    {
        public string title { get; set; } = "";
        public string description { get; set; } = "";
        public DateTime date { get; set; } 
        public string imagesName { get; set; } = "";
        public string eventLink { get; set; } = "";
        public string location { get; set; } = "";
    }
}
