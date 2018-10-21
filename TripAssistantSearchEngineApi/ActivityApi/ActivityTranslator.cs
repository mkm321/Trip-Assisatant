using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;

namespace TripAssistantSearchEngineApi
{
    public class ActivityTranslator : IActivityTranslator
    {
        readonly string url = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=";
        private static List<string> _staticPlaceType = new List<string>(new string[]{
            "amusement_park",
            "aquarium",
            "art_gallery",
            "church",
            "hindu_temple",
            "mosque",
            "museum",
            "park",
            "shopping_mall",
            "zoo",
            "natural_feature",
            "point_of_interest"
        });

        public List<Activity> GetFilteredActivity(List<JObject> activityjObject)
        {
            List<Activity> activityList = new List<Activity>();
            Activity activity = new Activity();
            List<ActivityList> tempData = new List<ActivityList>();
            ActivityList activityList1 = new ActivityList();
            JArray types = new JArray();

            foreach (JObject rest in activityjObject)
            {
                var results = rest["results"].Value<JArray>();
                activityList1 = new ActivityList();
                activity = new Activity();
                tempData = new List<ActivityList>();
                foreach (JObject res in results)
                {
                    types = res["types"].Value<JArray>();
                    foreach(string type in types)
                    {
                        if (_staticPlaceType.Contains(type))
                        {
                            activity.Type = type;
                            break;
                        }
                    }
                    activityList1 = SingleFilteredResult(res);
                    tempData.Add(activityList1);  
                }
                activity.ListActivity = tempData;
                if (activity.Type != null)
                {
                    activityList.Add(activity);
                }
            }
            return activityList;
        }
        public ActivityList SingleFilteredResult(JObject res)
        {
            ActivityList activity = new ActivityList();
            try {
                
                JObject activityDetails;
                string placeId = res["place_id"].Value<String>();
                activityDetails = res;
                activity.Name = activityDetails["name"].Value<String>();
                if (activityDetails["rating"] != null)
                {
                    activity.Rating = activityDetails["rating"].Value<Double>();
                }
                JObject photo = null;
                JObject geometry = activityDetails["geometry"].Value<JObject>();
                JObject location = geometry["location"].Value<JObject>();
                activity.Lattitude = location["lat"].Value<Double>();
                activity.Longitude = location["lng"].Value<Double>();
                if (activityDetails["photos"] != null)
                {
                    JArray photoArray = res["photos"].Value<JArray>();
                    photo = photoArray[0].Value<JObject>();
                    activity.PhotoUrl = url + photo["photo_reference"].Value<String>() + "&key=AIzaSyD2bL_pYSzue4JkSDQg4fYSuVT8XA_bjCQ";
                }
                
                activity.PlaceId = placeId;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                activity = null;
            }
            return activity;
        }

    }
    
}
