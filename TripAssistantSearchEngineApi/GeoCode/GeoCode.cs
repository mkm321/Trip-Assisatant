using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Contracts;

namespace TripAssistantSearchEngineApi
{
    public class GeoCode : IGeoCode
    {
        List<double> locations = new List<double>();
        string geoCode = "";
        private readonly IGeoCodeGenerator _geoCodeGenerator;
        private readonly string _startFieldOfCumulativeUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=";
        private readonly string _endFieldOfCumulativeUrl = "&radius=300000&keyword=point%20of%20interest&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";
        private readonly string _startFieldOfSingleUrl = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=";
        private readonly string _endFieldOfSingleUrl = "&key=AIzaSyAGsJD6XqB9zheEOUoYFpOCGuPuDlUWhOc";

        public GeoCode(IGeoCodeGenerator geoCodeGenerator)
        {
            _geoCodeGenerator = geoCodeGenerator;
        }

        public string GetCumulativeGeoCode(string geocode)
        {
            geoCode = "";
            try
            {
                string url = _startFieldOfCumulativeUrl + geocode + _endFieldOfCumulativeUrl;
                Task<List<double>> location = _geoCodeGenerator.GetGeoLocation(url);
                locations = location.Result;
                geoCode += (locations[0] / locations[2]).ToString() + ",";
                geoCode += (locations[1] / locations[2]).ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                geocode = "0,0";
            }
            return geoCode;
        }

        public string GetGeoCodeOfCity(string city)
        {
            geoCode = "";
            try
            {
                string url = _startFieldOfSingleUrl + city + _endFieldOfSingleUrl;
                Task<List<double>> location = _geoCodeGenerator.GetGeoLocation(url);
                locations = location.Result;
                geoCode += locations[0].ToString() + ",";
                geoCode += locations[1].ToString();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                geoCode = "0,0";
            }
            return geoCode;
        }
    }
    
}
