using System.Text.RegularExpressions;

namespace AviationApp.FAADataParser.Utils
{
    public static class ParseLatitudeLongitude
    {
        public static bool TryParse(string latLongString, out double latLong)
        {
            latLong = 0.0;
            Match matchLatitude = latitudeDegMinSecRegex.Match(latLongString);
            Match matchLongitude = longitudeDegMinSecRegex.Match(latLongString);
            Match matchAllSec = allSecRegex.Match(latLongString);
            if (matchLatitude.Success || matchLongitude.Success)
            {
                Match match = matchLatitude.Success ? matchLatitude : matchLongitude;
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
                latLong = (negative ? -1.0 : 1.0) * (degrees + (minutes / 60.0) + (seconds / 3600.0));
                return true;
            }
            else if (matchAllSec.Success)
            {
                Match match = matchAllSec;
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
                latLong = (negative ? -1.0 : 1.0) * seconds / 3600.0;
                return true;
            }
            return false;
        }
        private static readonly Regex latitudeDegMinSecRegex = new Regex(@"\b(?<Degrees>\d{2})-(?<Minutes>\d{2})-(?<Seconds>\d{2}\.\d{3})(?<Hemisphere>[NS])\b");
        private static readonly Regex longitudeDegMinSecRegex = new Regex(@"\b(?<Degrees>\d{3})-(?<Minutes>\d{2})-(?<Seconds>\d{2}\.\d{3})(?<Hemisphere>[EW])\b");
        private static readonly Regex allSecRegex = new Regex(@"\b(?<Seconds>\d{6}\.\d{3})(?<Hemisphere>[NSEW])\b");
    }
}
