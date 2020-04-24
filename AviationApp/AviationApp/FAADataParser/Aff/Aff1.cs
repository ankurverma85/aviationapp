using System;

using AviationApp.FAADataParser.Utils;

namespace AviationApp.FAADataParser.Aff
{
    class Aff1
    {
        public string ArtccIdent { get; set; }
        public string ArtccName { get; set; }
        public string SiteLocation { get; set; }
        public string AltName { get; set; }
        public FacilityType FacilityType { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string IcaoId { get; set; }

        public static bool TryParse(string recordString, out Aff1 aff1)
        {
            aff1 = new Aff1();
            if (recordString.Length != RECORD_LEN)
            {
                return false;
            }
            if (recordString.Substring(0, 4) != "AFF1")
            {
                return false;
            }
            aff1.ArtccIdent = recordString.Substring(ARTCC_IDENT_START, ARTCC_IDENT_LEN).Trim();
            aff1.ArtccName = recordString.Substring(ARTCC_NAME_START, ARTCC_NAME_LEN).Trim();
            aff1.SiteLocation = recordString.Substring(SITE_LOCATION_START, SITE_LOCATION_LEN).Trim();
            aff1.AltName = recordString.Substring(ALT_NAME_START, ALT_NAME_LEN).Trim();
            if (!FacilityTypeParser.TryParse(recordString.Substring(FACILITY_TYPE_START, FACILITY_TYPE_LEN).Trim(), out FacilityType? facilityType))
            {
                return false;
            }
            aff1.FacilityType = (FacilityType)facilityType;
            if (!DateTime.TryParseExact(recordString.Substring(EFFECTIVE_DATE_START, EFFECTIVE_DATE_LEN).Trim(), "mm/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime effectiveDate))
            {
                return false;
            }
            aff1.EffectiveDate = effectiveDate;
            aff1.StateName = recordString.Substring(SITE_STATE_NAME_START, SITE_STATE_NAME_LEN).Trim();
            aff1.StateCode = recordString.Substring(SITE_STATE_PO_CODE_START, SITE_STATE_PO_CODE_LEN).Trim();
            if (!ParseLatitudeLongitude.TryParse(recordString.Substring(SITE_LATITUDE_START, SITE_LATITUDE_LEN).Trim(), out decimal latitude))
            {
                return false;
            }
            aff1.Latitude = latitude;
            if (!ParseLatitudeLongitude.TryParse(recordString.Substring(SITE_LONGITUDE_START, SITE_LONGITUDE_LEN).Trim(), out decimal longitude))
            {
                return false;
            }
            aff1.Longitude = longitude;
            aff1.IcaoId = recordString.Substring(ICAO_ARTCC_ID_START, ICAO_ARTCC_ID_LEN).Trim();

            return true;
        }

        private const int RECORD_LEN = 254;
        private const int ARTCC_IDENT_START = 4;
        private const int ARTCC_IDENT_LEN = 4;
        private const int ARTCC_NAME_START = 8;
        private const int ARTCC_NAME_LEN = 40;
        private const int SITE_LOCATION_START = 48;
        private const int SITE_LOCATION_LEN = 30;
        private const int ALT_NAME_START = 78;
        private const int ALT_NAME_LEN = 50;
        private const int FACILITY_TYPE_START = 128;
        private const int FACILITY_TYPE_LEN = 5;
        private const int EFFECTIVE_DATE_START = 133;
        private const int EFFECTIVE_DATE_LEN = 10;
        private const int SITE_STATE_NAME_START = 143;
        private const int SITE_STATE_NAME_LEN = 30;
        private const int SITE_STATE_PO_CODE_START = 173;
        private const int SITE_STATE_PO_CODE_LEN = 2;
        private const int SITE_LATITUDE_START = 175;
        private const int SITE_LATITUDE_LEN = 14;
        private const int SITE_LONGITUDE_START = 200;
        private const int SITE_LONGITUDE_LEN = 14;
        private const int ICAO_ARTCC_ID_START = 225;
        private const int ICAO_ARTCC_ID_LEN = 4;
    }
}
