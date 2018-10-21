using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Data.Contract

{
    public class Activity
    {
        public string Type { get; set; }
        public List<ActivityList> ListActivity { get; set; }
    }
}
