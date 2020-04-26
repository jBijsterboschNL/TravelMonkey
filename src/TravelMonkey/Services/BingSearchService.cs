using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelMonkey.Models;

namespace TravelMonkey.Services
{
    public class BingSearchService
    {
        public async Task<IEnumerable<DestinationImage>> GetDestinationImages(string destination)
        {
            try
            {
                var client = new ImageSearchClient(new ApiKeyServiceClientCredentials(ApiKeys.BingImageSearch));

                var result = await client.Images.SearchAsync(destination, imageType: "Photo", minWidth: 600, maxWidth: 1200, minHeight: 400, maxHeight: 1200, count: 3);

                var images = new List<DestinationImage>();
                foreach (var image in result.Value)
                {
                    images.Add(new DestinationImage(image.ContentUrl, image.HostPageUrl));
                }

                return images;
            }
            catch
            {
                return new List<DestinationImage>
                {
                    new DestinationImage("https://cataas.com/cat", "https://cataas.com/")
                };
            }
        }
    }
}