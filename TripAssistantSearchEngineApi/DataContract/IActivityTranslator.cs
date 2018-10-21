using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IActivityTranslator
    {
        List<Activity> GetFilteredActivity(List<JObject> activityjObject);
    }
}
