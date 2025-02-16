using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.ConfigSections;
using CooperationHullMainSite.Models.SanityCMS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sanity.Linq;
using Sanity.Linq.BlockContent;
using Sanity.Linq.CommonTypes;

namespace CooperationHullMainSite.Services
{
    public class SanityCMSCalls : ISanityCMSCalls
    {
        private readonly ILogger<SanityCMSCalls> _logger;
        private static Olav.Sanity.Client.SanityClient _client;
        private SanityCMSConfig config { get; set; }

        public SanityCMSCalls(IOptions<SanityCMSConfig> configuration,
                                    ILogger<SanityCMSCalls> logger)
        {
            config = configuration.Value;
            _logger = logger;
            SetSanityClient();
        }


        public async Task<List<HappeningNextEvent>> GetHomePageEventsList()
        {
            var result = new List<HappeningNextEvent>();

            SanityImageConfigOptions imageConfigOptions = new SanityImageConfigOptions()
            {
                useRawImage = false,
                height = 200,
            };

            string comparisonDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            try
            {
                var itemList = await _client.Query<Event>($"*[_type == 'event' && date > '{comparisonDate}']|order(date asc){{ _id, title, description, date, location, eventLink, {SanityImageExtended.ImageQuery},  \"imageAltText\": image.asset -> altText }}[0...3]");

                if (itemList.Item1 == System.Net.HttpStatusCode.OK)
                {
                    foreach (Event item in itemList.Item2.Result)
                    {
                        var homePageEvent = new HappeningNextEvent()
                        {
                            title = item.title,
                            description = item.description,
                            date = item.date,
                            eventLink = item.eventLink,
                            imagesName = GenerateImageURL(item.image, imageConfigOptions),
                            imageAltText = item.imageAltText,
                            location = item.location
                        };

                        result.Add(homePageEvent);
                    }

                    result.OrderBy(x => x.date);

                }
                else
                {
                    _logger.LogError($"Error getting events from Sanity CMS {itemList.Item1.ToString()}");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting events from Sanity CMS");
                return new List<HappeningNextEvent>();
            }

            return result;

        }



        public async Task<List<PostSummary>> GetAllBlogPostsList()
        {

            var result = new List<PostSummary>();

            SanityImageConfigOptions imageConfigOptions = new SanityImageConfigOptions()
            {
                useRawImage = false,
                height = 200,
                width = 250,
                background = "F4E9E6"
            };

            try
            {
                var query = $"*[_type == 'blogPost']|order(date desc){{_id, title, author, date, slug, tags, summary," +
                            $" {SanityImageExtended.ImageQuery}, 'info': image.asset -> altText }}[1...10000]";

                var itemList = await _client.Query<BlogPostSummary>(query);

                if (itemList.Item1 == System.Net.HttpStatusCode.OK)
                {
                    foreach (BlogPostSummary item in itemList.Item2.Result)
                    {
                        var post = new PostSummary()
                        {
                            Title = item.title,
                            date = item.date,
                            slug = item.slug.Current,
                            author = item.author,
                            tags = item.tags,
                            summaryImageUrl = GenerateImageURL(item.image, imageConfigOptions),
                            imageAltText = item.imageAltText,
                            summaryText = item.summary
                        };

                        result.Add(post);
                    }

                }
                else
                {
                    _logger.LogError($"Error getting blog posts list from Sanity CMS {itemList.Item1.ToString()}");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting blog post lists from Sanity CMS");
            }
            return result;
        }

       public async Task<PostSummary> GetLatestBlogPostSummary()
        {

            SanityImageConfigOptions imageConfigOptions = new SanityImageConfigOptions()
            {
                useRawImage = false,
                height = 400,
                width = 800,
                background = "F4E9E6"
            };

            try
            {

                var query = $"*[_type == 'blogPost']|order(date desc){{_id, title, author, date, slug, tags, summary," +
                            $" {SanityImageExtended.ImageQuery}, 'info': image.asset -> altText }}[0]";

                var itemList = await _client.QuerySingle<BlogPostSummary>(query);

                if (itemList.Item1 == System.Net.HttpStatusCode.OK)
                {

                    BlogPostSummary item = itemList.Item2.Result;

                        var post = new PostSummary()
                        {
                            Title = item.title,
                            date = item.date,
                            slug = item.slug.Current,
                            author = item.author,
                            tags = item.tags,
                            summaryImageUrl = GenerateImageURL(item.image, imageConfigOptions),
                            imageAltText = item.imageAltText,
                            summaryText = item.summary
                        };

                    return post;
                    }

                else
                {
                    _logger.LogError($"Error getting blog posts list from Sanity CMS {itemList.Item1.ToString()}");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting blog post lists from Sanity CMS");
            }
            return new PostSummary();
        }

        public async Task<BlogPostContent> GetBlogPostDetails(string slug)
        {
            var result = new BlogPostContent();

            try
            {
                var temp = await _client.QuerySingle<BlogPostContent>($"*[_type == 'blogPost' && slug.current == '{slug}']{{_id, title, author, date, content, summary }}[0]");

                if (temp.Item1 == System.Net.HttpStatusCode.OK)
                {
                    result = temp.Item2.Result;

                    var builer = GetHtmlBuilder();

                    result.contentHtml = await builer.BuildAsync(result.content);
                }
                else
                {
                    _logger.LogError($"Error getting blog {slug} from Sanity CMS {temp.Item1.ToString()}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting blog from Sanity CMS");
            }

            return result;
        }


        public async Task<EventPageModel> GetEventsPageData()
        {
            var result = new EventPageModel();

            SanityImageConfigOptions imageConfigOptions = new SanityImageConfigOptions()
            {
                useRawImage = false,
                height = 200,
            };

            string comparisonDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            try
            {
                var itemList = await _client.Query<EventV2>($"*[_type == 'eventv2' && date > '{comparisonDate}']|order(date asc){{ _id, title, description, eventTags, duration, date, location, eventLink, {SanityImageExtended.ImageQuery},  \"imageAltText\": image.asset -> altText }}");

                if (itemList.Item1 == System.Net.HttpStatusCode.OK)
                {
                    foreach (EventV2 item in itemList.Item2.Result)
                    {
                        var homePageEvent = new EventItem()
                        {
                            title = item.title,
                            description = item.description,
                            date = item.date,
                            startTime = item.duration.start,
                            endTime = item.duration.end,
                            imagesName = GenerateImageURL(item.image, imageConfigOptions),
                            imageAltText = item.imageAltText,
                            tagData = String.Join(',', item.eventTags.Select(x => x.value).ToArray()),
                            locationLink = GenerateGoogleMapsURL(item.location)
                        };

                        result.events.Add(homePageEvent);
                        result.tags.AddRange(item.eventTags.Select(x => x.value));
                    }

                    result.events.OrderBy(x => x.date);

                }
                else
                {
                    _logger.LogError($"Error getting V2 events from Sanity CMS {itemList.Item1.ToString()}");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting V2 events from Sanity CMS");
                return new EventPageModel();
            }

            return result;

        }


        private void SetSanityClient()
        {
            var projectid = config.ProjectID;
            var datasetName = config.DatasetName;

            if (_client == null)
            {
                _client = new Olav.Sanity.Client.SanityClient(projectid, datasetName, null, false);
            }

        }

        private SanityHtmlBuilder GetHtmlBuilder()
        {
            var options = new SanityOptions();
            options.ProjectId = config.ProjectID;
            options.Dataset = config.DatasetName;

            return new SanityHtmlBuilder(options);
        }

        private string GenerateImageURL(SanityImageExtended imageData, SanityImageConfigOptions options)
        {

            if (imageData == null)
                return "";  //todo - add in default image

            string[] details = imageData.Asset.Ref.Split('-');
            var reference = details[1];
            var size = details[2];
            var type = details[3];

            if (options.useRawImage)
                return $"https://cdn.sanity.io/images/{config.ProjectID}/{config.DatasetName}/{reference}-{size}.{type}";

            try
            {

                List<string> queryParams = new List<string>();

                //Format conversion not yet supported
                //queryParams.Add("auto=format");

                if(imageData.Crop == null)
                {
                    if (options.height != -1)
                        queryParams.Add($"h={options.height}");
                    if (options.width != -1)
                        queryParams.Add($"w={options.width}");

                    queryParams.Add("fit=fill");
                }

                if (imageData.Crop != null && (options.height == -1 && options.width == -1))
                    queryParams.Add(CalcImageCropBasic(imageData));

                if (imageData.Crop != null && !(options.height == -1 && options.width == -1))
                    queryParams.AddRange(CalcImageCropHotSpot(imageData, options.height, options.width));

                if (options.background != "")
                    queryParams.Add($"bg={options.background}");

                switch (options.imageFlip)
                {
                    case ImageFlip.Horizontal:
                        queryParams.Add("flip=h");
                        break;
                    case ImageFlip.Vertical:
                        queryParams.Add("flip=v");
                        break;
                    case ImageFlip.Both:
                        queryParams.Add("flip=hv");
                        break;
                }

                var scaleQuery = string.Join("&", queryParams);

                var imageUrl = $"https://cdn.sanity.io/images/{config.ProjectID}/{config.DatasetName}/{reference}-{size}.{type}?{scaleQuery}";

                return imageUrl;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error generating adjusted image URL");
                return $"https://cdn.sanity.io/images/{config.ProjectID}/{config.DatasetName}/{reference}-{size}.{type}";
            }
        }

        private string GenerateGoogleMapsURL(SanityLocation location)
        {
            return "Link to location on google maps will go here";
        }
        private string CalcImageCropBasic(SanityImageExtended imageData)
        {
            // Compute crop rect in terms of pixel coordinates in the raw source image
            var cropLeft = Math.Round(imageData.Crop.Left * imageData.width);
            var cropTop = Math.Round(imageData.Crop.Top * imageData.height);
            var cropWidth = Math.Round(imageData.width - (imageData.Crop.Right * imageData.width) - cropLeft);
            var cropHeight = Math.Round(imageData.height - (imageData.Crop.Bottom * imageData.height) - cropTop);

            if (cropLeft != 0 || cropTop != 0 || cropHeight != imageData.height || cropWidth != imageData.width)
            {
                return ($"rect={cropLeft},{cropTop},{cropWidth},{cropHeight}");
            }

            return "";
        }

        private List<string> CalcImageCropHotSpot(SanityImageExtended imageData, int setHeight, int setWidth)
        {
            List<string> result = new List<string>();
            // If we are not constraining the aspect ratio, we'll just use the whole crop
            if (imageData.Hotspot == null || (setHeight == -1 || setWidth == -1))
            {
                result.Add(CalcImageCropBasic(imageData));

                if (setHeight != -1)
                    result.Add($"w={setHeight}");
                if (setWidth != -1)
                    result.Add($"h={setWidth}");

                return result;
            }

            //math copied over from https://github.com/sanity-io/image-url/blob/main/src/urlForImage.ts

            var cropLeft = Math.Round(imageData.Crop.Left * imageData.width);
            var cropTop = Math.Round(imageData.Crop.Top * imageData.height);
            var cropWidth = Math.Round(imageData.width - (imageData.Crop.Right * imageData.width) - cropLeft);
            var cropHeight = Math.Round(imageData.height - (imageData.Crop.Bottom * imageData.height) - cropTop);


            // If we are here, that means aspect ratio is locked and fitting will be a bit harder
            var desiredAspectRatio = setWidth / setHeight;
            var cropAspectRatio = cropWidth / cropHeight;

            var hotSpotVerticalRadius = Math.Round((imageData.Hotspot.Height * imageData.height) / 2);
            var hotSpotHorizontalRadius = Math.Round((imageData.Hotspot.Width * imageData.width) / 2);
            var hotSpotCenterX = Math.Round(imageData.Hotspot.X * imageData.width);
            var hotSpotCenterY = Math.Round(imageData.Hotspot.Y * imageData.height);

            var hotspotleft = hotSpotCenterX - hotSpotHorizontalRadius;
            var hotspottop = hotSpotCenterY - hotSpotVerticalRadius;
            var hotspotright = hotSpotCenterX + hotSpotHorizontalRadius;
            var hotspotbottom = hotSpotCenterY + hotSpotVerticalRadius;


            if (cropAspectRatio > desiredAspectRatio)
            {
                // The crop is wider than the desired aspect ratio. That means we are cutting from the sides

                var height = cropHeight;
                var width = Math.Round(height * desiredAspectRatio);
                var top = cropTop;

                // Center output horizontally over hotspot
                var hotspotXCenter = Math.Round((hotspotright - hotspotleft) / 2 + hotspotleft);

                var tempLeft = Math.Round(hotspotXCenter - width / 2);

                // Keep output within crop
                if (tempLeft < cropLeft)
                    tempLeft = cropLeft;
                else if (tempLeft + width > cropLeft + cropWidth)
                    tempLeft = cropLeft + cropWidth - width;

                result.Add($"rect={tempLeft},{top},{width},{height}");
            }
            else
            {
                // The crop is taller than the desired aspect ratio. That means we are cutting from the top and bottom
                var width = cropWidth;
                var height = Math.Round(width / desiredAspectRatio);
                var left = cropLeft;

                // Center output vertically over hotspot
                var hotspotYCenter = Math.Round((hotspotbottom - hotspottop) / 2 + hotspottop);
                var tempTop = Math.Round(hotspotYCenter - height / 2);

                // Keep output rect within crop
                if (tempTop < cropTop)
                    tempTop = cropTop;
                else if (tempTop + height > cropTop + cropHeight)
                    tempTop = cropTop + cropHeight - height;

                result.Add($"rect={left},{tempTop},{width},{height}");
            }

            if (setHeight != -1)
                result.Add($"h={setHeight}");
            if (setWidth != -1)
                result.Add($"w={setWidth}");

            // Compute hot spot rect in terms of pixel coordinates

            return result;
        }

    }
}
