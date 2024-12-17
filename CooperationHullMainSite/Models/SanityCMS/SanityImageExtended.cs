using Sanity.Linq.CommonTypes;

namespace CooperationHullMainSite.Models.SanityCMS
{
    public class SanityImageExtended : SanityImage
    {
        public const string ImageQuery = "image { ..., ...asset-> {altText, caption, ...metadata {lqip, ...dimensions {width, height}}} }";

        public int width { get; set; }
        public int height { get; set; }
        public string caption { get; set; }
        public string altText { get; set; }
        public string lqip { get; set; }

        SanityImageExtended()
        {
            width = 0;
            height = 0;
            caption = "";
            altText = "";
            lqip = "";
        }
    }


    public enum ImageFlip
    {
        None,
        Horizontal,
        Vertical,
        Both
    }

    public class SanityImageConfigOptions
    {
        public bool useRawImage { get; set; } = true;
        public string background { get; set; } = "F5A117";  // deafault to main background colour oraange if nothing else set
        public ImageFlip imageFlip { get; set; } = ImageFlip.None;
        public int height { get; set; } = -1;
        public int width { get; set; } = -1;
    }

}



