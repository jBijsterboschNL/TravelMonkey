using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;

namespace TravelMonkey.Models
{
    public class PictureEntry
    {
        public Guid Id { get; }

        public string Description { get; }
        
        public byte[] Image { get; }

        [JsonIgnore]
        public ImageSource ImageStream => ImageSource.FromStream(() => new MemoryStream(Image));

        public PictureEntry(string description, byte[] image)
        {
            Id = Guid.NewGuid();
            Description = description;
            Image = image;
        }
    }
}