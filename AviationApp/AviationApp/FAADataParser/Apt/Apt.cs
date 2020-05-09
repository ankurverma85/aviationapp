using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AviationApp.FAADataParser.Utils;
using AviationApp.Utilities.Units;

namespace AviationApp.FAADataParser.Apt
{
    class Apt
    {
        public LandingFacilityType LandingFacilityType { get; set; }
        public string LocationIdentifier { get; set; }
        public DateTime InformationEffectiveDate { get; set; }
        public string FAAFieldOffice { get; set; }
        public string StatePostOfficeCode { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CountyStatePostOfficeCode { get; set; }
        public string CityName { get; set; }
        public string FacilityName { get; set; }
        public AirportOwnershipType AirportOwnershipType { get; set; }
        public FacilityUse FacilityUse { get; set; }
        public string OwnersName { get; set; }
        public string OwnersAddress1 { get; set; }
        public string OwnersAddress2 { get; set; }
        [Phone]
        public string OwnersPhone { get; set; }
        public string ManagersName { get; set; }
        public string ManagersAddress1 { get; set; }
        public string ManagersAddress2 { get; set; }
        [Phone]
        public string ManagersPhone { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public SurveyMethod ReferencePointDeterminationMethod { get; set; }
        public decimal Elevation { get; set; }
        public SurveyMethod ElevationDeterminationMethod { get; set; }
        public int MagneticVariation { get; set; }
        public int MagneticVariationEpochYear { get; set; }
        public int? TPA { get; set; }
        public string SectionalName { get; set; }
        private int DistanceFromCBDInNm { set => DistanceFromCBD.NauticalMile = value; }
        public Length DistanceFromCBD { get; set; } = new Length();
        public CompassPoint DirectionFromCBD { get; set; }
        public string BoundaryArtccIdentifier { get; set; }
        public string BoundaryArtccFaaComputerIdentifier { get; set; }
        public string BoundaryArtccName { get; set; }
        public string ResponsibleArtccIdentifier { get; set; }
        public string ResponsibleArtccFaaComputerIdentifier { get; set; }
        public string ResponsibleArtccName { get; set; }
        public bool TieInFssOnFacility { get; set; }
        public string TieInFssIdentifier { get; set; }
        public string TieInFssName { get; set; }
        [Phone]
        public string LocalFssPhoneNumber { get; set; }
        [Phone]
        public string TollFreeFssPhoneNumber { get; set; }
        public string AlternateFssIdentifier { get; set; }
        public string AlternateFssName { get; set; }
        [Phone]
        public string TollFreeAlternateFssNumber { get; set; }
        public string NotamAndWeatherFacilityIdentifier { get; set; }
        public bool NotamDAvailableAtAirport { get; set; }

        public static bool TryParse(string val, out Apt apt)
        {
            return FAADataParserGeneric<Apt>.TryParse(val, 1529, "APT", fieldsList, out apt);
        }

        private static readonly List<(int, int, Type, string, bool)> fieldsList = new List<(int, int, Type, string, bool)>
        {
            (14, 13, typeof(LandingFacilityTypeParser), nameof(LandingFacilityType), false),
            (27, 4, typeof(string), nameof(LocationIdentifier), false),
            (31, 10,  typeof(ParseDate), nameof(InformationEffectiveDate), false),
            (44, 4, typeof(string), nameof(FAAFieldOffice), false),
            (48, 2, typeof(string), nameof(StatePostOfficeCode), false),
            (50, 20, typeof(string), nameof(StateName), false),
            (70, 21, typeof(string), nameof(CountyName), false),
            (91, 2, typeof(string), nameof(CountyStatePostOfficeCode), false),
            (93, 40, typeof(string), nameof(CityName), false),
            (133, 50, typeof(string), nameof(FacilityName), false),
            (183, 2, typeof(AirportOwnershipTypeParser), nameof(AirportOwnershipType), false),
            (185, 2, typeof(FacilityUseParser), nameof(FacilityUse), false),
            (187, 35, typeof(string), nameof(OwnersName), false),
            (222, 72, typeof(string), nameof(OwnersAddress1), false),
            (294, 45, typeof(string), nameof(OwnersAddress2), false),
            (339, 16, typeof(ParsePhone), nameof(OwnersPhone), false),
            (355, 35, typeof(string), nameof(ManagersName), false),
            (390, 72, typeof(string), nameof(ManagersAddress1), false),
            (462, 45, typeof(string), nameof(ManagersAddress2), false),
            (507, 16, typeof(ParsePhone), nameof(ManagersPhone), false),
            (523, 15, typeof(ParseLatitudeLongitude), nameof(Latitude), false),
            (550, 15, typeof(ParseLatitudeLongitude), nameof(Longitude), false),
            (577, 1, typeof(SurveyMethodParser), nameof(ReferencePointDeterminationMethod), false),
            (578, 7, typeof(ParseDecimal), nameof(Elevation), false),
            (585, 1, typeof(SurveyMethodParser), nameof(ElevationDeterminationMethod), false),
            (586, 3, typeof(MagVarParser), nameof(MagneticVariation), false),
            (589, 4, typeof(ParseInt), nameof(MagneticVariationEpochYear), false),
            (593, 4, typeof(ParseInt), nameof(TPA), true),
            (597, 30, typeof(string), nameof(SectionalName), false),
            (627, 2, typeof(ParseInt), nameof(DistanceFromCBDInNm), false),
            (629, 3, typeof(ParseCompassPoint), nameof(DirectionFromCBD), false),
            (637, 4, typeof(string), nameof(BoundaryArtccIdentifier), false),
            (641, 3, typeof(string), nameof(BoundaryArtccFaaComputerIdentifier), false),
            (644, 30, typeof(string), nameof(BoundaryArtccName), false),
            (674, 4, typeof(string), nameof(ResponsibleArtccIdentifier), false),
            (678, 3, typeof(string), nameof(ResponsibleArtccFaaComputerIdentifier), false),
            (681, 30, typeof(string), nameof(ResponsibleArtccName), false),
            (711, 1, typeof(ParseBool), nameof(TieInFssOnFacility), false),
            (712, 4, typeof(string), nameof(TieInFssIdentifier), false),
            (716, 30, typeof(string), nameof(TieInFssName), false),
            (746, 16, typeof(ParsePhone), nameof(LocalFssPhoneNumber), true),
            (762, 16, typeof(ParsePhone), nameof(TollFreeFssPhoneNumber), true),
            (778, 4, typeof(string), nameof(AlternateFssIdentifier), true),
            (782, 30, typeof(string), nameof(AlternateFssName), true),
            (812, 16, typeof(ParsePhone), nameof(TollFreeAlternateFssNumber), true),
            (828, 4, typeof(string), nameof(NotamAndWeatherFacilityIdentifier), false),
            (832, 1, typeof(ParseBool), nameof(NotamDAvailableAtAirport), false)
        };
    }
}
