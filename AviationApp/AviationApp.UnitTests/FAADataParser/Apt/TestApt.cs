using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AviationApp.FAADataParser.Apt;
using AviationApp.FAADataParser.Utils;

namespace AviationApp.UnitTests.FAADataParser.TestApt
{
    [TestClass]
    public class TestApt
    {
        [TestMethod]
        public void TestParseKBFI()
        {
            string record = @"APT26396.*A   AIRPORT      BFI 04/23/2020ANMSEA WAWASHINGTON          KING                 WASEATTLE                                 BOEING FIELD/KING COUNTY INTL                     PUPUKING COUNTY                        516 THIRD AVE                                                           SEATTLE, WA 98104                              (206) 296-7380JOHN PARROTT, AAE                  7277 PERIMETER ROAD SOUTH                                               SEATTLE, WA 98108-3844                         (206) 296-733447-31-47.9000N 171107.9000N122-18-07.0000W440287.0000WE   21.6S15E2020    SEATTLE                       04S    634ZSE ZCSSEATTLE                       ZSE ZCSSEATTLE                       NSEA SEATTLE                                       1-800-WX-BRIEF                                                    BFI Y11/1938O IV A S 01/1974 NGPY3  NOT ANALYZED YNNYF F06262019        100LLA                                  MAJORMAJORHIGH/LOWHIGH/LOWSEE RMKSS-SR  Y122.950       N   CG Y 229040088026001000000010142      02791304953709450000117601/01/20193RD PARTY SURVEY02/02/20163RD PARTY SURVEY02/02/2016 HGR,TIE     AFRT,AMB,AVNCS,CARGO,CHTR,INSTR,RNTL,SALES,SURV                        Y-LKBFI   N                                                                                                                                                                                                                                                                                                                       ";
            Assert.IsTrue(Apt.TryParse(record, out Apt apt));
            Assert.AreEqual(LandingFacilityType.Airport, apt.LandingFacilityType);
            Assert.AreEqual("BFI", apt.LocationIdentifier);
            Assert.AreEqual(new DateTime(2020, 4, 23), apt.InformationEffectiveDate);
            Assert.AreEqual("SEA", apt.FAAFieldOffice);
            Assert.AreEqual("WA", apt.StatePostOfficeCode);
            Assert.AreEqual("WASHINGTON", apt.StateName);
            Assert.AreEqual("KING", apt.CountyName);
            Assert.AreEqual("WA", apt.CountyStatePostOfficeCode);
            Assert.AreEqual("SEATTLE", apt.CityName);
            Assert.AreEqual("BOEING FIELD/KING COUNTY INTL", apt.FacilityName);
            Assert.AreEqual(AirportOwnershipType.PubliclyOwned, apt.AirportOwnershipType);
            Assert.AreEqual(FacilityUse.Public, apt.FacilityUse);
            Assert.AreEqual("KING COUNTY", apt.OwnersName);
            Assert.AreEqual("516 THIRD AVE", apt.OwnersAddress1);
            Assert.AreEqual("SEATTLE, WA 98104", apt.OwnersAddress2);
            Assert.AreEqual("+1 206-296-7380", apt.OwnersPhone);
            Assert.AreEqual("JOHN PARROTT, AAE", apt.ManagersName);
            Assert.AreEqual("7277 PERIMETER ROAD SOUTH", apt.ManagersAddress1);
            Assert.AreEqual("SEATTLE, WA 98108-3844", apt.ManagersAddress2);
            Assert.AreEqual("+1 206-296-7334", apt.ManagersPhone);
            Assert.AreEqual(171107.9000m, apt.Latitude);
            Assert.AreEqual(-440287.0000m, apt.Longitude);
            Assert.AreEqual(SurveyMethod.Estimated, apt.ReferencePointDeterminationMethod);
            Assert.AreEqual(21.6m, apt.Elevation);
            Assert.AreEqual(SurveyMethod.Surveyed, apt.ElevationDeterminationMethod);
            Assert.AreEqual(-15, apt.MagneticVariation);
            Assert.AreEqual(2020, apt.MagneticVariationEpochYear);
            Assert.IsNull(apt.TPA);
            Assert.AreEqual("SEATTLE", apt.SectionalName);
            Assert.AreEqual(04, apt.DistanceFromCBD.NauticalMile);
            Assert.AreEqual(CompassPoint.South, apt.DirectionFromCBD);
            Assert.AreEqual("ZSE", apt.BoundaryArtccIdentifier);
            Assert.AreEqual("ZCS", apt.BoundaryArtccFaaComputerIdentifier);
            Assert.AreEqual("SEATTLE", apt.BoundaryArtccName);
            Assert.AreEqual("ZSE", apt.ResponsibleArtccIdentifier);
            Assert.AreEqual("ZCS", apt.ResponsibleArtccFaaComputerIdentifier);
            Assert.AreEqual("SEATTLE", apt.ResponsibleArtccName);
            Assert.IsFalse(apt.TieInFssOnFacility);
            Assert.AreEqual("SEA", apt.TieInFssIdentifier);
            Assert.AreEqual("SEATTLE", apt.TieInFssName);
            Assert.IsNull(apt.LocalFssPhoneNumber);
            Assert.AreEqual("+1 800-992-7433", apt.TollFreeFssPhoneNumber);
            Assert.IsNull(apt.AlternateFssIdentifier);
            Assert.IsNull(apt.AlternateFssName);
            Assert.IsNull(apt.TollFreeAlternateFssNumber);
            Assert.AreEqual("BFI", apt.NotamAndWeatherFacilityIdentifier);
            Assert.IsTrue(apt.NotamDAvailableAtAirport);
        }
    }
}
