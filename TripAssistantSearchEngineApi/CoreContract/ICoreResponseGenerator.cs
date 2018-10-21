using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface ICoreResponseGenerator
    {
        Response MakeResponse(string input, Task<List<Hotel>> hotels, List<Activity> activities, string type, string res);
    }
}
