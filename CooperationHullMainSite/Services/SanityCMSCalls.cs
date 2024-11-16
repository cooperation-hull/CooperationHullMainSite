using CooperationHullMainSite.Models;
using CooperationHullMainSite.Models.ConfigSections;
using CooperationHullMainSite.Models.SanityCMS;
using Microsoft.Extensions.Options;
using Olav.Sanity.Client;
using Sanity.Linq.Extensions;

namespace CooperationHullMainSite.Services
{
    public class SanityCMSCalls : ISanityCMSCalls
    {
        private readonly ILogger<SanityCMSCalls> _logger;
        private SanityCMSConfig config { get; set; }

        public SanityCMSCalls(IOptions<SanityCMSConfig> configuration,
                                    ILogger<SanityCMSCalls> logger)
        {
            config = configuration.Value;
            _logger = logger;
        }

      public async  Task<List<HappeningNextEvent>> GetHomePageEventsList()
        {
            var result = new List<HappeningNextEvent>();

            try
            {
                var client = getSanityClient();

                var itemList = await client.Query<Event>("*[_type == 'event']{ _id, title, description, date, location, eventLink, \"image\": image.asset->url }");

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
                var client = getSanityClient();

                var itemList = await client.Query<BlogPostSummary>("*[_type == 'blogPost']{_id, title, author, date, slug, tags, summary, \"image\": image.asset->url }");

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
                    _logger.LogError($"Error getting events from Sanity CMS {itemList.Item1.ToString()}");
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting events from Sanity CMS");
            }
            return result;
        }
    public  async Task<bool> GetBlogPostDetails()
        {
            throw new NotImplementedException();
        }

        private SanityClient getSanityClient()
        {
            var projectid = config.ProjectId;
            var datasetName = config.DatasetName;

            var client = new SanityClient(projectid, datasetName, null, false);

            return client;
        }

    }
}
