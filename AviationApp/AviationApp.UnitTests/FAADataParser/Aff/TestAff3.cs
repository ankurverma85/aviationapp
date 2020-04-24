using System;
using System.Collections.Generic;
using System.Text;
using AviationApp.FAADataParser.Aff;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AviationApp.UnitTests.FAADataParser.Aff
{
    [TestClass]
    public class TestAff3
    {
        [TestMethod]
        public void TestRCAGWithAirport()
        {
            string input = @"AFF3ZAB ALAMOGORDO                    RCAG 132.65  LOW/HIGH                  YALM NEW MEXICO                    NMALAMOGORDO                              ALAMOGORDO-WHITE SANDS RGNL                       32-50-21.900N 118221.900N105-59-28.100W381568.100W";
            Assert.IsTrue(Aff3.TryParse(input, out Aff3 aff3));
            Assert.AreEqual("ZAB", aff3.ArtccIdent);
            Assert.AreEqual("ALAMOGORDO", aff3.SiteLocation);
            Assert.AreEqual(FacilityType.RemoteCommunicationsAirGround, aff3.FacilityType);
            Assert.AreEqual(132.65m, aff3.Frequency);
            Assert.AreEqual(AltitudeSector.LowHigh, aff3.AltitudeSector);
            Assert.AreEqual(FrequencySpecialUsage.None, aff3.FrequencySpecialUsage);
            Assert.IsNotNull(aff3.RCAGFrequencyCharted);
            Assert.IsTrue((bool)aff3.RCAGFrequencyCharted);
            Assert.AreEqual("ALM", aff3.AirportIdent);
            Assert.AreEqual("NEW MEXICO", aff3.AirportState);
            Assert.AreEqual("NM", aff3.AirportStatePOCode);
            Assert.AreEqual("ALAMOGORDO-WHITE SANDS RGNL", aff3.AirportName);
            Assert.AreEqual(118221.900m, aff3.AirportLatitude);
            Assert.AreEqual(-381568.100m, aff3.AirportLongitude);
        }
        [TestMethod]
        public void TestArtccNoAirport()
        {
            string input = @"AFF3ZAB ALBUQUERQUE                   ARTCC121.5   LOW/HIGH  DISCRETE                                                                                                                                                                                         ";
            Assert.IsTrue(Aff3.TryParse(input, out Aff3 aff3));
            Assert.AreEqual("ZAB", aff3.ArtccIdent);
            Assert.AreEqual("ALBUQUERQUE", aff3.SiteLocation);
            Assert.AreEqual(FacilityType.AirRouteTrafficControlCentre, aff3.FacilityType);
            Assert.AreEqual(121.5m, aff3.Frequency);
            Assert.AreEqual(AltitudeSector.LowHigh, aff3.AltitudeSector);
            Assert.AreEqual(FrequencySpecialUsage.Discrete, aff3.FrequencySpecialUsage);
            Assert.IsNull(aff3.RCAGFrequencyCharted);
            Assert.IsNull(aff3.AirportIdent);
            Assert.IsNull(aff3.AirportState);
            Assert.IsNull(aff3.AirportStatePOCode);
            Assert.IsNull(aff3.AirportName);
            Assert.IsNull(aff3.AirportLatitude);
            Assert.IsNull(aff3.AirportLongitude);
        }
    }
}
