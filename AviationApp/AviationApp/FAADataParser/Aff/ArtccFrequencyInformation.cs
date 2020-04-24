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
        public bool RCAGFrequencyCharted { get; set; }
        public bool AirportInformationAvailable { get; set; }
        public string AirportIdent { get; set; }
        public string AirportState { get; set; }
        public string AirportStatePOCode { get; set; }
        public string AirportCity { get; set; }
        public string AirportName { get; set; }
        public double AirportLatitude { get; set; }
        public double AirportLongitude { get; set; }
        public List<(int number, string remark)> Remarks { get; set; } = new List<(int number, string remark)>();
    }
}
