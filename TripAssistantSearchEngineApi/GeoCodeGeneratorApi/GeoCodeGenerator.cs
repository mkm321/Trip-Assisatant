using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Core.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class GeoCodeGenerator : IGeoCodeGenerator
    {
        public async Task<List<double>> GetGeoLocation(string url)
        {
            int count = 0;
            List<double> locations = new List<double>();
            JObject location, geometry;
            double latitude = 0;
            double longitude = 0;
            try
            {
                using (var client = new WebClient())
                {
                    string jsonPrediction = await client.DownloadStringTaskAsync(url);
                    var data = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                    var results = data["results"].Value<JArray>();
                    foreach (JObject res in results)
                    {
                        count++;
                        geometry = res["geometry"].Value<JObject>();
                        location = geometry["location"].Value<JObject>();
                        latitude += location["lat"].Value<Double>();
                        longitude += location["lng"].Value<Double>();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                latitude = 0;
                longitude = 0;
                count = 0;
            }
            locations.Add(latitude);
            locations.Add(longitude);
            locations.Add(count);
            return locations;
        }
    }
   
   
}
