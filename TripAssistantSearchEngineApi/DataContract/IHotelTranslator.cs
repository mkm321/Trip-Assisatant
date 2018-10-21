using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IHotelTranslator
    {
        List<Hotel> GetFilteredHotel(JObject hoteljObject);
    }
}
