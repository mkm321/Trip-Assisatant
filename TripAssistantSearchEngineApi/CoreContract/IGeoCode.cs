using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts
{
    public interface IGeoCode
    {
        string GetGeoCodeOfCity(string city);
        string GetCumulativeGeoCode(string geocode);
    }
}
