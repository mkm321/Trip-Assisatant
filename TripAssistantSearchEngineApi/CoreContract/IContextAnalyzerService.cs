using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IContextAnalyzerService
    {
        Response GetResultsFromApi(string input, string location);
    }
}
