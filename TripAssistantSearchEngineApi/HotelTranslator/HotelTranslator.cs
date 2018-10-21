using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TripAssistantSearchEngineApi
{
    public class HotelTranslator : IHotelTranslator
    {
        public List<Hotel> GetFilteredHotel(JObject hoteljObject)
        {
            var results = hoteljObject["results"].Value<JArray>();
            Hotel hotel;
            List<Hotel> hotelList = new List<Hotel>();

            foreach (JObject res in results)
            {
                hotel = new Hotel();

                hotel.Name = res["name"].Value<String>();
                hotel.Rating = res["rating"].Value<Double>();
                hotel.Address = res["vicinity"].Value<String>();
                hotel.Photos = res["photos"].Value<JArray>();
                JObject geometry = res["geometry"].Value<JObject>();
                JObject location = geometry["location"].Value<JObject>();
                hotel.Lattitude= location["lat"].Value<Double>();
                hotel.Longitude = location["lng"].Value<Double>();
                JArray hotelTypes = res["types"].Value<JArray>();
                hotel.Types = (String)hotelTypes[0];
                
                hotelList.Add(hotel);
            }
            
         
            return hotelList;
        }
    }
    public interface IHotelTranslator
    {
        List<Hotel> GetFilteredHotel(JObject hoteljObject);
    }
}
