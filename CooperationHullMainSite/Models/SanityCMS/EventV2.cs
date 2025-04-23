using Sanity.Linq.CommonTypes;

namespace CooperationHullMainSite.Models.SanityCMS
{
    public class EventV2
    {
        public SanityImageExtended image { get; set; }
        public string imageAltText { get; set; }
        public List<EventTags> eventTags { get; set; }
        public string title { get; set; }
        public DateOnly date { get; set; }
        public string description { get; set; }
        public string eventLink { get; set; }
        public CustomDuration duration { get; set; }
        public string locationName { get; set; }
        public string locationLink { get; set; }

    }
}
