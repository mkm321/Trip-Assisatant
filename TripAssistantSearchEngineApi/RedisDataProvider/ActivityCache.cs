using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripAssistantSearchEngineApi
{
    public class ActivityCache: IActivityCache
    { 
        public ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        public List<JObject> GetActivitiesFromCache(string city)
        {
            List<JObject> data = new List<JObject>();
            try
            {
                IDatabase db = redis.GetDatabase();
                string val = db.StringGet(city);
                if (val == null)
                {
                    return null;
                }
                data = JsonConvert.DeserializeObject<List<JObject>>(val);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                data = null;
            }
            return data;
        }
        public void InsertActivitiesInCache(List<JObject> activity, string city)
        {
            try
            {
                IDatabase db = redis.GetDatabase();
                string data = JsonConvert.SerializeObject(activity);
                if (db.StringSet(city, data))
                {
                    var val = db.StringGet(city);
                    Console.WriteLine(val);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Remove(string key)
        {
            try
            {
                IDatabase db = redis.GetDatabase();
                db.KeyDelete(key);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }       
    }    
}

