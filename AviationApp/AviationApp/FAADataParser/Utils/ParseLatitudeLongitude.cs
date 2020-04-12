using System;
using System.Text.RegularExpressions;

namespace AviationApp.FAADataParser.Utils
{
    public static class ParseLatitudeLongitude
    {
        public static double TryParse(string latitudeString)
        {
            Match match = latlonRegex.Match(latitudeString);
            if (!match.Success)
            {
                throw new ArgumentException("Bad latitude");
            }
            if (!int.TryParse(match.Groups["Degrees"].Value, out int degrees))
            {
                throw new ApplicationException("Bad latitude");
            }
            if (!int.TryParse(match.Groups["Minutes"].Value, out int minutes))
            {
                throw new ApplicationException("Bad latitude");
            }
            if (!double.TryParse(match.Groups["Seconds"].Value, out double seconds))
            {
                throw new ApplicationException("Bad latitude");
            }
            bool negative;
            switch (match.Groups["Hemisphere"].Value)
            {
                case "N": negative = false; break;
                case "S": negative = true; break;
                case "E": negative = false; break;
                case "W": negative = true; break;
                default: throw new ApplicationException("Invalid hemisphere");
            }
            double latitude = (negative ? -1.0 : 1.0) * (degrees + (minutes/60.0) + (seconds/3600.0));
            return latitude;
        }
        private static readonly Regex latlonRegex = new Regex(@"(?<Degrees>\d*)-(?<Minutes>\d*)-(?<Seconds>\d*\.\d*)(?<Hemisphere>[NSEW])\s*");
    }
}
