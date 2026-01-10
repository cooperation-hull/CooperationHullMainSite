namespace CooperationHullMainSite.Models
{

    public class HomePageModel
    {
        public string title { get; set; } = "";
        public List<EventItem> eventList { get; set; } = new List<EventItem>();
        public HomePageModel() { }
    }

}
