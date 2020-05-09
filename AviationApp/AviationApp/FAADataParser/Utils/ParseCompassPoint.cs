using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace AviationApp.FAADataParser.Utils
{
    enum CompassPoint
    {
        North,
        NorthNorthEast,
        NorthEast,
        EastNorthEast,
        East,
        EastSouthEast,
        SouthEast,
        SouthSouthEast,
        South,
        SouthSouthWest,
        SouthWest,
        WestSouthWest,
        West,
        WestNorthWest,
        NorthWest,
        NorthNorthWest
    };
    class ParseCompassPoint
    {
        public static bool TryParse(string input, out CompassPoint compassPoint)
        {
            compassPoint = CompassPoint.North;
            switch (input)
            {
                case "N": compassPoint = CompassPoint.North; break;
                case "NNE": compassPoint = CompassPoint.NorthNorthEast; break;
                case "NE": compassPoint = CompassPoint.NorthEast; break;
                case "ENE": compassPoint = CompassPoint.EastNorthEast; break;
                case "E": compassPoint = CompassPoint.East; break;
                case "ESE": compassPoint = CompassPoint.EastSouthEast; break;
                case "SE": compassPoint = CompassPoint.SouthEast; break;
                case "SSE": compassPoint = CompassPoint.SouthSouthEast; break;
                case "S": compassPoint = CompassPoint.South; break;
                case "SSW": compassPoint = CompassPoint.SouthSouthWest; break;
                case "SW": compassPoint = CompassPoint.SouthWest; break;
                case "WSW": compassPoint = CompassPoint.WestSouthWest; break;
                case "W": compassPoint = CompassPoint.West; break;
                case "WNW": compassPoint = CompassPoint.WestNorthWest; break;
                case "NW": compassPoint = CompassPoint.NorthWest; break;
                case "NNW": compassPoint = CompassPoint.NorthNorthWest; break;
                default: return false;
            }
            return true;
        }
    }
}
