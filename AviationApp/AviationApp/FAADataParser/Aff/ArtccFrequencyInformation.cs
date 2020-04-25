using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Aff
{
    public class ArtccFrequencyInformation
    {
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
    }
}
