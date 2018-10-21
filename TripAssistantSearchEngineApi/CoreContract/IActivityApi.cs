using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IActivityApi
    {
        List<Activity> GetActivities(string location, string city);
        Task<JObject> GetActivitiesByPlaceId(string placeId);
    }
}
