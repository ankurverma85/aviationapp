using System;
using System.Collections.Generic;
using System.Text;

namespace AviationApp.FAADataParser.Aff
{
    public class ArtccFacility
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
        public List<(int number, string remark)> FacilityRemarks { get; set; } = new List<(int number, string remark)>();
        public List<ArtccFrequencyInformation> Frequencies { get; set; } = new List<ArtccFrequencyInformation>();
    }
}
