using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public class Hotel
    {
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public double Rating { get; set; }
        public string Address { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
