using System;
using System.Collections.Generic;
using System.Text;
using AviationApp.FAADataParser.Fixes;
using AviationApp.FAADataParser.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AviationApp.UnitTests.FAADataParser.Utils
{
    [TestClass]
    public class TestFAADataParserGeneric
    {
        [TestMethod]
        public void TestParseFix1()
        {
            List<(int fieldBegin, int fieldLength, Type parserType, string propertyName, bool nullable)> parseFields = new List<(int fieldBegin, int fieldLength, Type parserType, string propertyName, bool nullable)>
            {
                (4, 30, typeof(string), nameof(Fix1.FixID), false),
                (34, 30, typeof(string), nameof(Fix1.State), false),
                (64, 2, typeof(string), nameof(Fix1.ICAORegionCode), false),
                (66, 14, typeof(ParseLatitudeLongitude), nameof(Fix1.Latitude), false),
                (80, 14, typeof(ParseLatitudeLongitude), nameof(Fix1.Longitude), false)
            };
            string fix1String = @"FIX1AARTA                         ALABAMA                       K734-36-21.290N 087-16-24.750WFIX                                                                                                                   YWAYPOINT       AARTAZME ZME                               NNN                                                                                                                                                                                                ";

            Assert.IsTrue(FAADataParserGeneric<Fix1>.TryParse(fix1String, 466, parseFields, out Fix1 fix));
            // Try parsing a real string from the database
            Assert.AreEqual("AARTA", fix.FixID);
            Assert.AreEqual("ALABAMA", fix.State);
            Assert.AreEqual("K7", fix.ICAORegionCode);
            Assert.AreEqual(124581.290m, fix.Latitude);
            Assert.AreEqual(-314184.750m, fix.Longitude);
            // Assert.AreEqual(FixType.Civilian, fix.FixType);
            // Assert.AreEqual(FixUse.Waypoint, fix.FixUse);
            // Assert.AreEqual("AARTA", fix.NASIdentifier);
            // Assert.AreEqual("ZME", fix.LowARTCC);
            // Assert.AreEqual("ZME", fix.HighARTCC);
        }
    }
}
