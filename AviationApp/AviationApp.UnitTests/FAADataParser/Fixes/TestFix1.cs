using System.Collections.Generic;

using AviationApp.FAADataParser.Fixes;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AviationApp.UnitTests.FAADataParser.Fixes
{
    [TestClass]
    public class TestFix1
    {
        [TestMethod]
        public void TestParse()
        {
            Fix1 fix;
            // Empty string should not parse
            Assert.IsFalse(Fix1.TryParse(@"", out fix));
            // Try parsing a real string from the database
            Assert.IsTrue(Fix1.TryParse(@"FIX1AARTA                         ALABAMA                       K734-36-21.290N 087-16-24.750WFIX                                                                                                                   YWAYPOINT       AARTAZME ZME                               NNN                                                                                                                                                                                                ", out fix));
            Assert.AreEqual("AARTA", fix.FixID);
            Assert.AreEqual("ALABAMA", fix.State);
            Assert.AreEqual("K7", fix.ICAORegionCode);
            Assert.AreEqual(34.60591389, fix.Latitude, 0.00000001);
            Assert.AreEqual(-87.27354167, fix.Longitude, 0.00000001);
            Assert.AreEqual(FixType.Civilian, fix.FixType);
            Assert.AreEqual(FixUse.Waypoint, fix.FixUse);
            Assert.AreEqual("AARTA", fix.NASIdentifier);
            Assert.AreEqual("ZME", fix.LowARTCC);
            Assert.AreEqual("ZME", fix.HighARTCC);
            // Fix5 string should not parse
            Assert.IsFalse(Fix1.TryParse(@"FIX5AARTA                         ALABAMA                       K7IAP                                                                                                                                                                                                                                                                                                                                                                                                             ", out fix));
        }
    }
}
