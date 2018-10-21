using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TripAssistantSearchEngineApi
{
    public class TripResultsService : ITripResultsService
    {
        private readonly IContextAnalyzerService _getcontextResults;
        public TripResultsService(IContextAnalyzerService contextAnalyzer)
        {
            _getcontextResults = contextAnalyzer;
        }
        public Response FetchResultsFromAPI(string input, string location)
        {
            Response finalResponseToController = new Response();
            Response coreResponse = _getcontextResults.GetResultsFromApi(input, location);
            finalResponseToController.Type = coreResponse.Type;
            if (coreResponse.Type == "req")
            {
                finalResponseToController.ActivityList = null;
                finalResponseToController.HotelList = null;
                finalResponseToController.Request = input;
                finalResponseToController.ResponseQuery = coreResponse.ResponseQuery;
            }
            else
            {
                finalResponseToController.ActivityList = coreResponse.ActivityList;
                finalResponseToController.HotelList = coreResponse.HotelList;
                finalResponseToController.Request = null;
                finalResponseToController.ResponseQuery = null;
            }
            return finalResponseToController;
        }
    }   
}
