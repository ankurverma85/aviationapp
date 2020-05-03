using System.Text.RegularExpressions;

namespace AviationApp.FAADataParser.Utils
{
    public static class ParseLatitudeLongitude
    {
        public static bool TryParse(string latLongString, out decimal latLong)
        {
            latLong = 0.0m;
            Match matchLatitude = latitudeDegMinSecRegex.Match(latLongString);
            Match matchLongitude = longitudeDegMinSecRegex.Match(latLongString);
            Match matchAllSec = allSecRegex.Match(latLongString);
            if (matchLatitude.Success || matchLongitude.Success)
            {
                Match match = matchLatitude.Success ? matchLatitude : matchLongitude;
                if (!decimal.TryParse(match.Groups["Degrees"].Value, out decimal degrees))
                {
                    return false;
                }
                if (!decimal.TryParse(match.Groups["Minutes"].Value, out decimal minutes))
                {
                    return false;
                }
                if (!decimal.TryParse(match.Groups["Seconds"].Value, out decimal seconds))
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
                latLong = (negative ? -1.0m : 1.0m) * ((degrees * 3600m) + (minutes * 60m) + seconds);
                return true;
            }
            else if (matchAllSec.Success)
            {
                Match match = matchAllSec;
                if (!decimal.TryParse(match.Groups["Seconds"].Value, out decimal seconds))
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
                latLong = (negative ? -1.0m : 1.0m) * seconds;
                return true;
            }
            return false;
        }
        private static readonly Regex latitudeDegMinSecRegex = new Regex(@"\b(?<Degrees>\d{2})-(?<Minutes>\d{2})-(?<Seconds>\d{2}\.\d{3,4})(?<Hemisphere>[NS])\b");
        private static readonly Regex longitudeDegMinSecRegex = new Regex(@"\b(?<Degrees>\d{3})-(?<Minutes>\d{2})-(?<Seconds>\d{2}\.\d{3,4})(?<Hemisphere>[EW])\b");
        private static readonly Regex allSecRegex = new Regex(@"\b(?<Seconds>\d{6}\.\d{3,4})(?<Hemisphere>[NSEW])\b");
    }
}
