using Core.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class CoreResponseGenerator : ICoreResponseGenerator
    {
        Response coreResponse = new Response();
        public Response MakeResponse(string input,Task<List<Hotel>> hotels, List<Activity> activities, string type, string res)
        {
            if (hotels != null)
            {
                coreResponse.HotelList = hotels.Result;
            }
            else
            {
                coreResponse.HotelList = null;
            }
            coreResponse.ActivityList = activities;
            coreResponse.Type = type;
            coreResponse.ResponseQuery = res;
            return coreResponse;
        }
    }
    
}
