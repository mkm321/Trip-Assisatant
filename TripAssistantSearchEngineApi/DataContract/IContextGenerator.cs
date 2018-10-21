using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IContextGenerator
    {
        string GetContextResponse(string input);
    }
}
