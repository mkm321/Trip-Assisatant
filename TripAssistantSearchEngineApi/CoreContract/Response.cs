using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public class Response
    {
        public string Type { get; set; }
        public List<Activity> ActivityList { get; set; }
        public List<Hotel> HotelList { get; set; }
        public string Request { get; set; }
        public string ResponseQuery { get; set; }
    }
}
