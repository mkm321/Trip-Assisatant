using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contract
{
    public interface IGeoCode
    {
        string GetGeoCodeOfCity(string city);
        string GetCumulativeGeoCode(string geocode);
    }
}
