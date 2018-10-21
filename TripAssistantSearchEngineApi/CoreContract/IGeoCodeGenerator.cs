using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IGeoCodeGenerator
    {
        Task<List<double>> GetGeoLocation(string url);
    }
}
