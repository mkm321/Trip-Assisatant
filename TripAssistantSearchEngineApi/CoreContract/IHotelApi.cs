using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IHotelApi
    {
        Task<List<Hotel>> GetHotelDetails(string queryString, string city);
    }
}
