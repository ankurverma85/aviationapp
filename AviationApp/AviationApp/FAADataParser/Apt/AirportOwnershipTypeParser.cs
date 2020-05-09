using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Apt
{
    enum AirportOwnershipType
    {
        PubliclyOwned,
        PrivatelyOwned,
        AirforceOwned,
        NavyOwned,
        ArmyOwned,
        CoastGuardOwned
    }
    class AirportOwnershipTypeParser
    {
        public static bool TryParse(string val, out AirportOwnershipType airportOwnershipType)
        {
            switch(val)
            {
                case "PU": airportOwnershipType = AirportOwnershipType.PubliclyOwned; break;
                case "PR": airportOwnershipType = AirportOwnershipType.PrivatelyOwned; break;
                case "MA": airportOwnershipType = AirportOwnershipType.AirforceOwned; break;
                case "MN": airportOwnershipType = AirportOwnershipType.NavyOwned; break;
                case "MR": airportOwnershipType = AirportOwnershipType.ArmyOwned; break;
                case "CG": airportOwnershipType = AirportOwnershipType.CoastGuardOwned; break;
                default: airportOwnershipType = AirportOwnershipType.PrivatelyOwned; return false;
            }
            return true;
        }
    }
}
