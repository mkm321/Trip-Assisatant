using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Core.Contracts;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class ActivityApi : IActivityApi
    {
        JObject activityResult;
        private readonly IActivityTranslator _activityTranslator;
        //private readonly IActivityCache _activityCache;
        public ActivityApi(IActivityTranslator activityTranslator)//, IActivityCache activityCache)
        {
            _activityTranslator = activityTranslator;
           // _activityCache = activityCache;
        }
        //Getting activities from the api and sending the filtered response.
        public List<Core.Contracts.Activity> GetActivities(string location, string city)
        {
            List<Core.Contracts.Activity> translatedActivityResult;
            List<JObject> activities = null; ;
            //activities = _activityCache.GetActivitiesFromCache(city);
            if (activities != null)
            {
                //_activityCache.Remove(city);
                translatedActivityResult = _activityTranslator.GetFilteredActivity(activities);
            }
            else
            {
                Task<List<JObject>> combinedResultsFromApi = FetchDataFromAllAPIs(location);
                //_activityCache.InsertActivitiesInCache(combinedResultsFromApi.Result,city);
                translatedActivityResult = _activityTranslator.GetFilteredActivity(combinedResultsFromApi.Result);
            }
            return translatedActivityResult;
        }

        public async Task<JObject> GetActivitiesByPlaceId(string placeId)
        {
            try
            {
                string url = "https://maps.googleapis.com/maps/api/place/details/json?place_id=" + placeId + "&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                using (WebClient client = new WebClient())
                {
                    string jsonPrediction = await client.DownloadStringTaskAsync(url);
                    activityResult = (JObject)JsonConvert.DeserializeObject(jsonPrediction);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                activityResult = null;
                
            }
            return activityResult;
        }
        public Task<List<JObject>> FetchDataFromAllAPIs(string location)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Task<List<JObject>>totalResponse = FetchSerializedData(location);
            while (totalResponse.Result.Count != 13) ;
            sw.Stop();
            return totalResponse;
        }
        //Calling multiple apis parallely 
        private async Task<List<JObject>> FetchSerializedData(string location)
        {
            List<JObject> searlizedResponse = new List<JObject>();
            string url;
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    Parallel.Invoke
                        (new ParallelOptions() { MaxDegreeOfParallelism = 1 },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=activity&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=attractions&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=amusement%20parks&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=aquarium&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=art%20gallery&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=church&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=hindu%20temple&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=mosque&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=museum&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=park&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=shopping%20mall&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=zoo&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=natural%20feature&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            },
                            () =>
                            {
                                url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&radius=150000&keyword=adventures&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
                                using (WebClient wc = new WebClient())
                                {
                                    wc.DownloadStringTaskAsync(new Uri(url));
                                    wc.DownloadStringCompleted += (sender, e) => {
                                        JObject result = JsonConvert.DeserializeObject<JObject>(e.Result);
                                        if (result != null)
                                        {
                                            searlizedResponse.Add(result);
                                        }
                                    };
                                }
                            }
                        );

                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                searlizedResponse = null;
            }
            return searlizedResponse;
        }
    }
    
}
