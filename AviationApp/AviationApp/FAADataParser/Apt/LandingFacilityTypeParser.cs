using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Apt
{
    public enum LandingFacilityType
    {
        Airport,
        Balloonport,
        SeaplaneBase,
        Gliderport,
        Heliport,
        Ultralight
    };
    class LandingFacilityTypeParser
    {
        public static bool TryParse(string val, out LandingFacilityType landingFacilityType)
        {
            switch(val)
            {
                case "AIRPORT": landingFacilityType = LandingFacilityType.Airport; break;
                case "BALLOONPORT": landingFacilityType = LandingFacilityType.Balloonport; break;
                case "SEAPLANE BASE": landingFacilityType = LandingFacilityType.SeaplaneBase; break;
                case "GLIDERPORT": landingFacilityType = LandingFacilityType.Gliderport; break;
                case "HELIPORT": landingFacilityType = LandingFacilityType.Heliport; break;
                case "ULTRALIGHT": landingFacilityType = LandingFacilityType.Ultralight; break;
                default: landingFacilityType = LandingFacilityType.Airport;  return false;
            }
            return true;
        }
    }
}
