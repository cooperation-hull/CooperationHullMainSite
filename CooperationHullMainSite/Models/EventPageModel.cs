namespace CooperationHullMainSite.Models
{
    public class EventPageModel
    {
      public  EventPageModel() { }
        public List<string> tags = new List<string>();
        public List<EventItem> events = new List<EventItem>();
    }


    public class EventItem
    {
        public string title;
        public string description;
        public DateOnly date { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string imagesName { get; set; } = "";
        public string imageAltText { get; set; } = "";
        public string eventLink { get; set; } = "";
        public string locationLink { get; set; }
        public string tagData { get; set; }
    }


}
