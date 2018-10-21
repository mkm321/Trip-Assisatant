using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Core.Contracts

{
    public class Activity
    {
        public string Type { get; set; }
        public List<ActivityList> ListActivity { get; set; }
    }
}
