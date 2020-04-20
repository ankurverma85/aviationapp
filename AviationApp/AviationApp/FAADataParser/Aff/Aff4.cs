namespace AviationApp.FAADataParser.Aff
{
    class Aff4
    {
        public string ArtccIdent { get; set; }
        public string SiteLocation { get; set; }
        public FacilityType FacilityType { get; set; }
        public decimal Frequency { get; set; }
        public int RemarksNumber { get; set; }
        public string RemarksText { get; set; }

        public static bool TryParse(string recordString, out Aff4 aff4)
        {
            aff4 = new Aff4();
            if (recordString.Length != RECORD_LEN)
            {
                return false;
            }
            if (recordString.Substring(0, 4) != "AFF4")
            {
                return false;
            }
            aff4.ArtccIdent = recordString.Substring(ARTCC_IDENT_START, ARTCC_IDENT_LEN).Trim();
            aff4.SiteLocation = recordString.Substring(SITE_LOCATION_START, SITE_LOCATION_LEN).Trim();
            if (!FacilityTypeParser.TryParse(recordString.Substring(FACILITY_TYPE_START, FACILITY_TYPE_LEN).Trim(), out FacilityType? facilityType))
            {
                return false;
            }
            aff4.FacilityType = (FacilityType)facilityType;
            if (!decimal.TryParse(recordString.Substring(FREQUENCY_START, FREQUENCY_LEN).Trim(), out decimal frequency))
            {
                return false;
            }
            aff4.Frequency = frequency;
            if (!int.TryParse(recordString.Substring(FREQ_REMARKS_NUM_START, FREQ_REMARKS_NUM_LEN).Trim(), out int remarksNumber))
            {
                return false;
            }
            aff4.RemarksNumber = remarksNumber;
            aff4.RemarksText = recordString.Substring(FREQ_REMARKS_TEXT_START, FREQ_REMARKS_TEXT_LEN).Trim();
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
        private const int FREQ_REMARKS_NUM_START = 51;
        private const int FREQ_REMARKS_NUM_LEN = 2;
        private const int FREQ_REMARKS_TEXT_START = 53;
        private const int FREQ_REMARKS_TEXT_LEN = 200;
    }
}
