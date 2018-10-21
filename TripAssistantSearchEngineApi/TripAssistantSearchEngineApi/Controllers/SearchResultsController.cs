using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreContract = Core.Contracts;
using DataContract = Data.Contract;
using AutoMapper;

namespace TripAssistantSearchEngineApi
{
    [Produces("application/json")]
    [Route("api/SearchResults")]
    public class SearchResultsController : Controller
    {
        private readonly CoreContract.ITripResultsService _tripResults;
        private readonly IMapper _mapper;
        public SearchResultsController(CoreContract.ITripResultsService iTripResult, IMapper mapper)
        {
            _tripResults = iTripResult;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetSearchResults([FromQuery] string input, [FromQuery] string location)
        {
            try
            {
                CoreContract.Response response = new CoreContract.Response();
                response = _tripResults.FetchResultsFromAPI(input, location);
                return Ok(_mapper.Map<DataContract.Response>(response));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Exception Occured!!!");
            }
        }

    }
}