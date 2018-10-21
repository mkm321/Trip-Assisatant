using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IContextAnalyzerService
    {
        Response GetResultsFromApi(string input, string location);
    }
}
