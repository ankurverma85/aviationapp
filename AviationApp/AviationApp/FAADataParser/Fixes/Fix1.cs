
using AviationApp.FAADataParser.Utils;

using SQLite;

namespace AviationApp.FAADataParser.Fixes
{
    public enum FixType { Civilian, Military };
    public enum FixUse
    {
        ComputerNavigationFix,
        MilitaryReportingPoint,
        MilitaryWaypoint,
        NRSWaypoint,
        Radar,
        ReportingWaypoint,
        VFRWaypoint,
        Waypoint
    };
    public class Fix1
    {
        [PrimaryKey, AutoIncrement]
        public ulong RecordId { get; set; }
        // Foreign key
        public int Cycle { get; set; }
        [MaxLength(FIXID_LEN)]
        public string FixID { get; set; }
        [MaxLength(STATE_NAME_LEN)]
        public string State { get; set; }
        [MaxLength(ICAO_CODE_LEN)]
        public string ICAORegionCode { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public FixType FixType { get; set; }
        public FixUse FixUse { get; set; }
        [MaxLength(5)]
        public string NASIdentifier { get; set; }
        [MaxLength(4)]
        public string HighARTCC { get; set; }
        [MaxLength(4)]
        public string LowARTCC { get; set; }

        public static bool TryParse(string recordString, out Fix1 fix1)
        {
            fix1 = new Fix1();
            if (recordString.Length != LOGICAL_RECORD_LENGTH)
            {
                return false;
            }
            if (recordString.Substring(0, 4) != "FIX1")
            {
                return false;
            }
            fix1.FixID = recordString.Substring(FIXID_START, FIXID_LEN).Trim();
            fix1.State = recordString.Substring(STATE_NAME_START, STATE_NAME_LEN).Trim();
            fix1.ICAORegionCode = recordString.Substring(ICAO_CODE_START, ICAO_CODE_LEN).Trim();
            if (!ParseLatitudeLongitude.TryParse(recordString.Substring(LATITUDE_START, LAT_LON_LEN).Trim(), out decimal latitude))
            {
                return false;
            }
            fix1.Latitude = latitude;
            if (!ParseLatitudeLongitude.TryParse(recordString.Substring(LONGITUDE_START, LAT_LON_LEN).Trim(), out decimal longitude))
            {
                return false;
            }
            fix1.Longitude = longitude;
            switch (recordString.Substring(FACILITY_TYPE_START, FACILITY_TYPE_LEN).Trim())
            {
                case "FIX": fix1.FixType = FixType.Civilian; break;
                case "MIL": fix1.FixType = FixType.Military; break;
                default: return false;
            }
            switch (recordString.Substring(FIX_USE_START, FIX_USE_LEN).Trim())
            {
                case "CNF": fix1.FixUse = FixUse.ComputerNavigationFix; break;
                case "MIL-REP-PT": fix1.FixUse = FixUse.MilitaryReportingPoint; break;
                case "MIL-WAYPOINT": fix1.FixUse = FixUse.MilitaryWaypoint; break;
                case "NRS_WAYPOINT": fix1.FixUse = FixUse.NRSWaypoint; break;
                case "RADAR": fix1.FixUse = FixUse.Radar; break;
                case "REP-PT": fix1.FixUse = FixUse.ReportingWaypoint; break;
                case "VFR-WP": fix1.FixUse = FixUse.VFRWaypoint; break;
                case "WAYPOINT": fix1.FixUse = FixUse.Waypoint; break;
                default: return false;
            }
            fix1.NASIdentifier = recordString.Substring(NAS_IDENTIFIER_START, NAS_IDENTIFIER_LEN).Trim();
            fix1.HighARTCC = recordString.Substring(HIGH_ARTCC_START, ARTCC_LEN).Trim();
            fix1.LowARTCC = recordString.Substring(LOW_ARTCC_START, ARTCC_LEN).Trim();
            return true;
        }

        private const int FIXID_START = 4;
        private const int FIXID_LEN = 30;
        private const int STATE_NAME_START = 34;
        private const int STATE_NAME_LEN = 30;
        private const int ICAO_CODE_START = 64;
        private const int ICAO_CODE_LEN = 2;
        private const int LATITUDE_START = 66;
        private const int LONGITUDE_START = 80;
        private const int LAT_LON_LEN = 14;
        private const int FACILITY_TYPE_START = 94;
        private const int FACILITY_TYPE_LEN = 3;
        private const int FIX_USE_START = 213;
        private const int FIX_USE_LEN = 15;
        private const int NAS_IDENTIFIER_START = 228;
        private const int NAS_IDENTIFIER_LEN = 5;
        private const int HIGH_ARTCC_START = 233;
        private const int LOW_ARTCC_START = 237;
        private const int ARTCC_LEN = 4;
        private const int LOGICAL_RECORD_LENGTH = 466;
    }
}
