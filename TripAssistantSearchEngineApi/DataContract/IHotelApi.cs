using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contract
{
    public interface IHotelApi
    {
        Task<List<Hotel>> GetHotelDetails(string queryString, string city);
    }
}
