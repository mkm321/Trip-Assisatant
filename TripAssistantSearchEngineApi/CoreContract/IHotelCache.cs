using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IHotelCache
    {
        void InsertHotelsInCache(JObject hotel, string city);
        JObject GetHotelsFromCache(string city);

    }
}
