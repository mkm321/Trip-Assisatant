using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IContextGenerator
    {
        string GetContextResponse(string input);
    }
}
