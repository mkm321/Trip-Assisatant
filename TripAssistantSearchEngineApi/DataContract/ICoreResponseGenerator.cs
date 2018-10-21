using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contract
{
    public interface ICoreResponseGenerator
    {
        Response MakeResponse(string input, Task<List<Hotel>> hotels, List<Activity> activities, string type, string res);
    }
}
