using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Apt
{
    enum FacilityUse { Public, Private };
    class FacilityUseParser
    {
        public static bool TryParse(string val, out FacilityUse facilityUse)
        {
            switch (val)
            {
                case "PU": facilityUse = FacilityUse.Public; break;
                case "PR": facilityUse = FacilityUse.Private; break;
                default: facilityUse = FacilityUse.Private; return false;
            }
            return true;
        }
    }
}
