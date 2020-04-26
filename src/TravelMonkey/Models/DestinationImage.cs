namespace TravelMonkey.Models
{
    public class DestinationImage
    {
        public string ImageUrl { get; }

        public string MoreInfoUrl { get; }

        public string Description { get; set; }

        public DestinationImage(string imageUrl, string moreInfoUrl)
        {
            ImageUrl = imageUrl;
            MoreInfoUrl = moreInfoUrl;
        }
    }
}