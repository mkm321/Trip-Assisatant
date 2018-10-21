using Core.Contracts;
using System.Linq;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class UserPreferencesService : IUserPreferenceService
    {
        List<Activities> activities = new List<Activities>()
        {
            new Activities {Points = 20, Type = "amusement_park"},
            new Activities {Points = 5, Type = "aquarium"},
            new Activities {Points = 0, Type = "art_gallery"},
            new Activities {Points = 0, Type = "church"},
            new Activities {Points = 0, Type = "hindu_temple"},
            new Activities {Points = 0, Type = "mosque"},
            new Activities {Points = 0, Type = "museum"},
            new Activities {Points = 10, Type = "park"},
            new Activities {Points = 25, Type = "shopping_mall"},
            new Activities {Points = 30, Type = "zoo"},
            new Activities {Points = 0, Type = "natural_feature"},
            new Activities {Points = 20, Type = "point_of_interest"}
        };
        public List<Activity> GetFilteredResultsBasedOnUserPreferences(List<Activity> activityList)
        {
            List<Activity> filteredActivityResult = new List<Activity>();
            List<Activities> list =  activities.OrderByDescending(x => x.Points).ToList();
            foreach(Activities activitie in list)
            {
                foreach(Activity activity in activityList)
                {
                    if (activity.Type.Equals(activitie.Type))
                    {
                        filteredActivityResult.Add(activity);
                    }
                }
            }
            return filteredActivityResult;
        }
    }
}