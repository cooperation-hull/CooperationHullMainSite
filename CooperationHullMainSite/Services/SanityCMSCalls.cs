using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.ConfigSections;
using CooperationHullMainSite.Models.SanityCMS;
using Microsoft.Extensions.Options;
using Sanity.Linq;
using Sanity.Linq.BlockContent;

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


        public async  Task<List<HappeningNextEvent>> GetHomePageEventsList()
        {
            var result = new List<HappeningNextEvent>();

            try
            {

                var itemList = await _client.Query<Event>("*[_type == 'event']{ _id, title, description, date, location, eventLink, \"image\": image.asset->url }");

                if(itemList.Item1 == System.Net.HttpStatusCode.OK)
                {
                    foreach(Event item in itemList.Item2.Result)
                    {
                        var homePageEvent = new HappeningNextEvent()
                        {
                            title = item.title,
                            description = item.description,
                            date = item.date,
                            eventLink = item.eventLink,
                            imagesName = item.image,
                            location = item.location
                        };

                        result.Add(homePageEvent);
                    }

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


     public  async Task<List<PostSummary>> GetBlogPostsList()
        {

            var result = new List<PostSummary>();

            try
            {
                var itemList = await _client.Query<BlogPostSummary>("*[_type == 'blogPost']{_id, title, author, date, slug, tags, summary, \"image\": image.asset->url }");

                if (itemList.Item1 == System.Net.HttpStatusCode.OK)
                {
                    foreach (BlogPostSummary item in itemList.Item2.Result)
                    {
                        var post = new PostSummary()
                        {
                            Title = item.title,
                            date = item.date,
                            slug = item.slug,
                            author = item.author,
                            tags = item.tags,
                            summaryImageUrl = item.image,
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
        public async Task<BlogPostContent> GetBlogPostDetails(string slug)
        {
            var result = new BlogPostContent();

            try 
            {
                var temp = await _client.QuerySingle<BlogPostContent>($"*[_type == 'blogPost' && slug == '{slug}']{{_id, title, author, date, content, summary }}[0]");

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

    }
}
