using System;
using System.Collections.Generic;

namespace TravelMonkey.Models
{
    public class Destination
    {
        public Guid Id { get; }

        public string Name { get; }

        public IEnumerable<DestinationImage> Images { get; } = new List<DestinationImage>();

        public double? Emotion { get; set; }

        public Destination(string name, IEnumerable<DestinationImage> images = null)
        {
            Id = Guid.NewGuid();
            Name = name;

            if (images != null)
            {
                Images = images;
            }
        }
    }
}