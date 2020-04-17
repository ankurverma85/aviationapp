using System.Text.RegularExpressions;

namespace AviationApp.FAADataParser.Utils
{
    public static class ParseLatitudeLongitude
    {
        public static bool TryParse(string latitudeString, out double latitude)
        {
            latitude = 0.0;
            Match match = latlonRegex.Match(latitudeString);
            if (!match.Success)
            {
                return false;
            }
            if (!int.TryParse(match.Groups["Degrees"].Value, out int degrees))
            {
                return false;
            }
            if (!int.TryParse(match.Groups["Minutes"].Value, out int minutes))
            {
                return false;
            }
            if (!double.TryParse(match.Groups["Seconds"].Value, out double seconds))
            {
                return false;
            }
            bool negative;
            switch (match.Groups["Hemisphere"].Value)
            {
                case "N": negative = false; break;
                case "S": negative = true; break;
                case "E": negative = false; break;
                case "W": negative = true; break;
                default: return false;
            }
            latitude = (negative ? -1.0 : 1.0) * (degrees + (minutes / 60.0) + (seconds / 3600.0));
            return true;
        }
        private static readonly Regex latlonRegex = new Regex(@"(?<Degrees>\d*)-(?<Minutes>\d*)-(?<Seconds>\d*\.\d*)(?<Hemisphere>[NSEW])\s*");
    }
}
