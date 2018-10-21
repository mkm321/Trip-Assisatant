using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contract
{
    public interface IGeoCodeGenerator
    {
        Task<List<double>> GetGeoLocation(string url);
    }
}
