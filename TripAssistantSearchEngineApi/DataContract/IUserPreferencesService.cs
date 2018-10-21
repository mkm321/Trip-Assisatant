using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IUserPreferencesService
    {
        List<Activity> GetFilteredResultsBasedOnUserPreferences(List<Activity> activityList);
    }
}
