using Core.Contracts;
using TripAssistantSearchEngineApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using static TripAssistantSearchEngineApi.HotelsApi;

namespace TripAssistantSearchEngineApi
{
    public class ContextAnalyzerService : IContextAnalyzerService
    {
        private readonly IGeoCode _geoCode;
        private readonly IActivityApi _activityApi;
        private readonly IUserPreferenceService _userPreferenceService;
        private readonly ICoreResponseGenerator _coreResponseGenerator;
        private readonly IHotelApi _hotelApi;
        private readonly IContextCheckerService _contextChecker;
        string typeResponse = "";
        List<Activity> activityResponse = null;
        Task<List<Hotel>> hotelResponse = null;
        string resultantResponse = "";
        string[] response;
        string splitResponse;
        string geoCode;
        public ContextAnalyzerService(IUserPreferenceService userPreferenceService,ICoreResponseGenerator coreResponseGenerator, IActivityApi activityApi,IGeoCode geoCode,IContextCheckerService contextChecker, IHotelApi hotelApi)
        {
            _userPreferenceService = userPreferenceService;
            _hotelApi = hotelApi;
            _activityApi = activityApi;
            _coreResponseGenerator = coreResponseGenerator;
            _contextChecker = contextChecker;
            _geoCode = geoCode;
        }
        public Response GetResultsFromApi(string input, string location)
        {
            splitResponse = _contextChecker.GetFilteredQueryResponse(input);
            response = splitResponse.Split(' ');
            try
            {
                if (response[0].Equals("yes"))
                {
                    typeResponse = "res";
                    string queryString = PerformOperationAgainstCorrectInput(location);
                    string[] keywords = queryString.Split(' ');
                    geoCode = _geoCode.GetGeoCodeOfCity(keywords[0]);
                    activityResponse = _activityApi.GetActivities(geoCode, keywords[0]);
                    activityResponse = _userPreferenceService.GetFilteredResultsBasedOnUserPreferences(activityResponse);
                    if (keywords[1] != "1")
                    {
                        queryString = _geoCode.GetCumulativeGeoCode(geoCode);
                        hotelResponse = _hotelApi.GetHotelDetails(queryString, keywords[0]);
                    }
                }
                else if (response[0].Equals("no"))
                {
                    typeResponse = "req";
                    resultantResponse = PerformOperationAgainstIncorrectInput();
                }
                else
                {
                    typeResponse = "req";
                    resultantResponse = "This Request was beyond my power!!!";
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                typeResponse = "req";
                hotelResponse = null;
                activityResponse = null;
                resultantResponse = "This Request is beyond my power!!!";
            }
            Response finalResults = new Response();
            finalResults= _coreResponseGenerator.MakeResponse(input,hotelResponse, activityResponse, typeResponse, resultantResponse);
            return finalResults;
        }
        public string PerformOperationAgainstCorrectInput(string location)
        {
            string queryString = "";
            if (response[response.Length - 1].Equals("current"))
            {
                queryString = location + " " + response[1];
            }
            else
            {
                queryString = response[1] + " " + response[2];
            }
            return queryString;
        }
        public string PerformOperationAgainstIncorrectInput()
        {
            resultantResponse = "";
            for (int index = 1; index < response.Length; index++)
            {
                resultantResponse += response[index] + " ";
            }
            return resultantResponse;
        }
    }    
}
