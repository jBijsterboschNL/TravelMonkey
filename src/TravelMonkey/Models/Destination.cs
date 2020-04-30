using System;
using System.Collections.Generic;

namespace TravelMonkey.Models
{
    public class Destination
    {
        public Guid Id { get; }
        
        public string IdString => Id.ToString();

        public string Name { get; }

        public IEnumerable<DestinationImage> Images { get; } = new List<DestinationImage>();

        public double? Emotion { get; set; }

        public Destination(string name, IEnumerable<DestinationImage> images = null, Guid? id = null)
        {
            if (id == null) id = Guid.NewGuid();

            Id = id.Value;
            Name = name;

            if (images != null)
            {
                Images = images;
            }
        }
    }
}