using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IActivityCache
    {
        List<JObject> GetActivitiesFromCache(string city);
        void InsertActivitiesInCache(List<JObject> activity, string city);
        void Remove(string key);

    }
}
