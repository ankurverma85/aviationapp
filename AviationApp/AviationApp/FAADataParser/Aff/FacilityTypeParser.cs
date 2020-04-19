using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Aff
{
    public enum FacilityType
    {
        AirRouteSurveillanceRadar,
        AirRouteTrafficControlCentre,
        CentreRadarApproachControlFacility,
        RemoteCommunicationsAirGround,
        SecondaryRadar
    };
    class FacilityTypeParser
    {
        public static bool TryParse(string value, out FacilityType? facilityType)
        {
            facilityType = null;
            switch (value)
            {
                case "ARSR": facilityType = FacilityType.AirRouteSurveillanceRadar; break;
                case "ARTCC": facilityType = FacilityType.AirRouteTrafficControlCentre; break;
                case "CERAP": facilityType = FacilityType.CentreRadarApproachControlFacility; break;
                case "RCAG": facilityType = FacilityType.RemoteCommunicationsAirGround; break;
                case "SECRA": facilityType = FacilityType.SecondaryRadar; break;
                default: return false;
            }
            return true;
        }
    }
}
