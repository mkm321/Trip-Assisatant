using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IContextCheckerService
    {
        string GetFilteredQueryResponse(string input);
    }
}
