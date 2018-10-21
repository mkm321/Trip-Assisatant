using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface ITripResultsService
    {
        Response FetchResultsFromAPI(string input, string location);
    }
}
