using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi.DataAccessLayer
{
    public class HotelApi
    {
        public async Task<Newtonsoft.Json.Linq.JObject> GetHotelsAsync(string queryString)
        {
            string url = "";
            // List<JObject> data = new List<JObject>();
            JObject data = new JObject();
            using (var client = new WebClient())
            {
                url = "  https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + queryString + "&radius=100000&type=hotels&keyword=hotels&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";


             
                var jsonPrediction = client.DownloadString(url);

                data = (JObject)JsonConvert.DeserializeObject(jsonPrediction);

            }
            return data;
        }
    }
}

