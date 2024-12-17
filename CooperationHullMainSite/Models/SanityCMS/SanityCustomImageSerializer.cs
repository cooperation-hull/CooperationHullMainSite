using Newtonsoft.Json.Linq;
using Sanity.Linq;
using Sanity.Linq.BlockContent;
using System.Text;

namespace CooperationHullMainSite.Models.SanityCMS
{

    public class SanityCustomImageSerializer : SanityHtmlSerializers
    {

        //FOr use with the html serializer when I've figured out how to do custom image types for the blog artivel pages.

       //protected override Task<string> SerializeImageAsync(JToken input, SanityOptions options)
       // {
       //     var asset = input["asset"];
       //     var imageRef = asset?["_ref"]?.ToString();

       //     if (asset == null || imageRef == null)
       //     {
       //         return Task.FromResult("");
       //     }

       //     var parameters = "h=500";

       //     if (input["query"] != null)
       //     {
       //         parameters = "?";
       //         parameters += (string)input["query"];
       //     }

       //     //build url
       //     var imageParts = imageRef.Split('-');
       //     var url = "https://cdn.sanity.io/";
       //     url += imageParts[0] + "s/";            // images/
       //     url += options.ProjectId + "/";             // projectid/
       //     url += options.Dataset + "/";             // dataset/
       //     url += imageParts[1] + "-";             // asset id-
       //     url += imageParts[2] + ".";             // dimensions.
       //     url += imageParts[3];                       // file extension
       //     url += "?h=500&w=500";                          // ?crop etc..

       //     return Task.FromResult(@"<figure><img src=""" + url + @""" /></figure>");
       // }


    }
}
