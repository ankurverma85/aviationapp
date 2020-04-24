using AviationApp.FAADataParser.Utils;

namespace AviationApp.FAADataParser.Aff
{
    public enum AltitudeSector { Low, High, LowHigh, UltraHigh };
    public enum FrequencySpecialUsage { None, ApproachControl, Discrete, DoNotPublish, Oceanic };

    public class Aff3
    {
        public string ArtccIdent { get; set; }
        public string SiteLocation { get; set; }
        public FacilityType FacilityType { get; set; }
        public decimal Frequency { get; set; }
        public AltitudeSector AltitudeSector { get; set; }
        public FrequencySpecialUsage FrequencySpecialUsage { get; set; }
        public bool? RCAGFrequencyCharted { get; set; } = null;
        public string AirportIdent { get; set; } = null;
        public string AirportState { get; set; } = null;
        public string AirportStatePOCode { get; set; } = null;
        public string AirportCity { get; set; } = null;
        public string AirportName { get; set; } = null;
        public decimal? AirportLatitude { get; set; } = null;
        public decimal? AirportLongitude { get; set; } = null;
        public static bool TryParse(string recordString, out Aff3 aff3)
        {
            aff3 = new Aff3();
            if (recordString.Length != RECORD_LEN)
            {
                return false;
            }
            if (recordString.Substring(0, 4) != "AFF3")
            {
                return false;
            }
            aff3.ArtccIdent = recordString.Substring(ARTCC_IDENT_START, ARTCC_IDENT_LEN).Trim();
            aff3.SiteLocation = recordString.Substring(SITE_LOCATION_START, SITE_LOCATION_LEN).Trim();
            if (!FacilityTypeParser.TryParse(recordString.Substring(FACILITY_TYPE_START, FACILITY_TYPE_LEN).Trim(), out FacilityType? facilityType))
            {
                return false;
            }
            aff3.FacilityType = (FacilityType)facilityType;
            if (!decimal.TryParse(recordString.Substring(FREQUENCY_START, FREQUENCY_LEN).Trim(), out decimal freq))
            {
                return false;
            }
            aff3.Frequency = freq;
            switch (recordString.Substring(ALTITUDE_START, ALTITUDE_LEN).Trim())
            {
                case "LOW": aff3.AltitudeSector = AltitudeSector.Low; break;
                case "HIGH": aff3.AltitudeSector = AltitudeSector.High; break;
                case "LOW/HIGH": aff3.AltitudeSector = AltitudeSector.LowHigh; break;
                case "ULTRA-HIGH": aff3.AltitudeSector = AltitudeSector.UltraHigh; break;
                default: return false;
            }
            switch (recordString.Substring(SPECIAL_USAGE_NAME_START, SPECIAL_USAGE_NAME_LEN).Trim())
            {
                case "": aff3.FrequencySpecialUsage = FrequencySpecialUsage.None; break;
                case "APPROACH CONTROL": aff3.FrequencySpecialUsage = FrequencySpecialUsage.ApproachControl; break;
                case "DISCRETE": aff3.FrequencySpecialUsage = FrequencySpecialUsage.Discrete; break;
                case "DO NOT PUBLISH": aff3.FrequencySpecialUsage = FrequencySpecialUsage.DoNotPublish; break;
                case "OCEANIC": aff3.FrequencySpecialUsage = FrequencySpecialUsage.Oceanic; break;
                default: return false;
            }
            string RCAGFrequencyCharted = recordString.Substring(RCAG_FREQUENCY_CHARTED_START, RCAG_FREQUENCY_CHARTED_LEN).Trim();
            if (facilityType == FacilityType.RemoteCommunicationsAirGround)
            {
                switch (RCAGFrequencyCharted)
                {
                    case "Y": aff3.RCAGFrequencyCharted = true; break;
                    case "N": aff3.RCAGFrequencyCharted = false; break;
                    default: return false;
                }
            }
            else if (RCAGFrequencyCharted != "")
            {
                return false;
            }
            if (recordString.Substring(AIRPORT_LOCATION_IDENTIFIER_START).Trim() == "")
            {
                return true;
            }
            aff3.AirportIdent = recordString.Substring(AIRPORT_LOCATION_IDENTIFIER_START, AIRPORT_LOCATION_IDENTIFIER_LEN).Trim();
            aff3.AirportState = recordString.Substring(AIRPORT_STATE_NAME_START, AIRPORT_STATE_NAME_LEN).Trim();
            aff3.AirportStatePOCode = recordString.Substring(AIRPORT_STATE_PO_CODE_START, AIRPORT_STATE_PO_CODE_LEN).Trim();
            aff3.AirportCity = recordString.Substring(AIRPORT_CITY_NAME_START, AIRPORT_CITY_NAME_LEN).Trim();
            aff3.AirportName = recordString.Substring(AIRPORT_NAME_START, AIRPORT_NAME_LEN).Trim();
            if (!ParseLatitudeLongitude.TryParse(recordString.Substring(AIRPORT_LATITUDE_START, AIRPORT_LATITUDE_LEN).Trim(), out decimal latitude))
            {
                return false;
            }
            aff3.AirportLatitude = latitude;
            if (!ParseLatitudeLongitude.TryParse(recordString.Substring(AIRPORT_LONGITUDE_START, AIRPORT_LONGITUDE_LEN).Trim(), out decimal longitude))
            {
                return false;
            }
            aff3.AirportLongitude = longitude;

            return true;
        }
        private const int RECORD_LEN = 254;
        private const int ARTCC_IDENT_START = 4;
        private const int ARTCC_IDENT_LEN = 4;
        private const int SITE_LOCATION_START = 8;
        private const int SITE_LOCATION_LEN = 30;
        private const int FACILITY_TYPE_START = 38;
        private const int FACILITY_TYPE_LEN = 5;
        private const int FREQUENCY_START = 43;
        private const int FREQUENCY_LEN = 8;
        private const int ALTITUDE_START = 51;
        private const int ALTITUDE_LEN = 10;
        private const int SPECIAL_USAGE_NAME_START = 61;
        private const int SPECIAL_USAGE_NAME_LEN = 16;
        private const int RCAG_FREQUENCY_CHARTED_START = 77;
        private const int RCAG_FREQUENCY_CHARTED_LEN = 1;
        private const int AIRPORT_LOCATION_IDENTIFIER_START = 78;
        private const int AIRPORT_LOCATION_IDENTIFIER_LEN = 4;
        private const int AIRPORT_STATE_NAME_START = 82;
        private const int AIRPORT_STATE_NAME_LEN = 30;
        private const int AIRPORT_STATE_PO_CODE_START = 112;
        private const int AIRPORT_STATE_PO_CODE_LEN = 2;
        private const int AIRPORT_CITY_NAME_START = 114;
        private const int AIRPORT_CITY_NAME_LEN = 40;
        private const int AIRPORT_NAME_START = 154;
        private const int AIRPORT_NAME_LEN = 50;
        private const int AIRPORT_LATITUDE_START = 204;
        private const int AIRPORT_LATITUDE_LEN = 14;
        private const int AIRPORT_LONGITUDE_START = 229;
        private const int AIRPORT_LONGITUDE_LEN = 14;
    }
}
