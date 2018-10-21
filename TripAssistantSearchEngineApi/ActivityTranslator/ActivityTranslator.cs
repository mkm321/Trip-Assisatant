using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;

namespace TripAssistantSearchEngineApi
{
    public class ActivityTranslator : IActivityTranslator
    {
        private IActivityApi _iActivity;
        public ActivityTranslator(IActivityApi activityResults)
        {
            this._iActivity = activityResults;

        }
         public List<Activity> GetFilteredActivity(JObject activityjObject)
        {
            List<Activity> activityList = new List<Activity>();
            Activity activity = null;
            JObject activityData,activityDetails;

          
            var results = activityjObject["results"].Value<JArray>();
            string placeUrl = "";
            foreach (JObject res in results)
            {
                activity = new Activity();

                string placeId = res["place_id"].Value<String>();
                activityData = _iActivity.GetActivitiesByPlaceId(placeId);
                activityDetails = activityData["result"].Value<JObject>();
                activity.Name = activityDetails["name"].Value<String>();
                activity.Address = activityDetails["formatted_address"].Value<String>();
                if (activityDetails["rating"] != null)
                {
               
                    activity.Rating = activityDetails["rating"].Value<Double>();
                }
                if (activityDetails["photos"]!= null)
                {
                    activity.PhotosUrl = activityDetails["photos"].Value<JArray>();
                }
              
                JObject geometry = activityDetails["geometry"].Value<JObject>();
                JObject location = geometry["location"].Value<JObject>();
                activity.Lattitude = location["lat"].Value<Double>();
                activity.Longitude = location["lng"].Value<Double>();
                JArray types = new JArray();
                types = activityDetails["types"].Value<JArray>();
                activity.Type = (String)types[0];

                if (activityDetails["opening_hours"] != null)
                {
                    activity.AvailableDays = activityDetails["opening_hours"].Value<JObject>()["weekday_text"].Value<JArray>();
                }
                activityList.Add(activity);
            }

          return activityList;
        }
    }
    public interface IActivityTranslator
    {
        List<Activity> GetFilteredActivity(JObject activityjObject);
    }
}
