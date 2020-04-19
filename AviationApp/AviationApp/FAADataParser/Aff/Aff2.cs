using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Aff
{
    class Aff2
    {
        public string ArtccIdent { get; set; }
        public string SiteLocation { get; set; }
        public FacilityType FacilityType { get; set; }
        public int RemarksNumber { get; set; }
        public string RemarksText { get; set; }

        public static bool TryParse(string recordString, out Aff2 aff2)
        {
            aff2 = new Aff2();
            if (recordString.Length != RECORD_LEN)
            {
                return false;
            }
            if (recordString.Substring(0, 4) != "AFF2")
            {
                return false;
            }
            aff2.ArtccIdent = recordString.Substring(ARTCC_IDENT_START, ARTCC_IDENT_LEN).Trim();
            aff2.SiteLocation = recordString.Substring(SITE_LOCATION_START, SITE_LOCATION_LEN).Trim();
            if (!FacilityTypeParser.TryParse(recordString.Substring(FACILITY_TYPE_START, FACILITY_TYPE_LEN).Trim(), out FacilityType? facilityType))
            {
                return false;
            }
            aff2.FacilityType = (FacilityType)facilityType;
            if (!int.TryParse(recordString.Substring(SITE_REMARKS_NUM_START, SITE_REMARKS_NUM_LEN).Trim(), out int remarksNumber))
            {
                return false;
            }
            aff2.RemarksNumber = remarksNumber;
            aff2.RemarksText = recordString.Substring(SITE_REMARKS_TEXT_START, SITE_REMARKS_TEXT_LEN).Trim();
            return true;
        }

        private const int RECORD_LEN = 254;
        private const int ARTCC_IDENT_START = 4;
        private const int ARTCC_IDENT_LEN = 4;
        private const int SITE_LOCATION_START = 8;
        private const int SITE_LOCATION_LEN = 30;
        private const int FACILITY_TYPE_START = 38;
        private const int FACILITY_TYPE_LEN = 5;
        private const int SITE_REMARKS_NUM_START = 43;
        private const int SITE_REMARKS_NUM_LEN = 4;
        private const int SITE_REMARKS_TEXT_START = 47;
        private const int SITE_REMARKS_TEXT_LEN = 200;
    }
}
