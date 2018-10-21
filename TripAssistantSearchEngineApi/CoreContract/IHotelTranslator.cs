using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IHotelTranslator
    {
        List<Hotel> GetFilteredHotel(JObject hoteljObject);
    }
}
