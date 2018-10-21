using Newtonsoft.Json.Linq;
using System;

namespace Data.Contract
{
    public class ActivityList
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string PlaceId { get; set; }
        public double Rating { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
