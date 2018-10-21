using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface ITripResultsService
    {
        Response FetchResultsFromAPI(string input, string location);
    }
}
