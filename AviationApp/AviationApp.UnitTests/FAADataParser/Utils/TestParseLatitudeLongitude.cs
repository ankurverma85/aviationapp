using System.Collections.Generic;

using AviationApp.FAADataParser.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AviationApp.UnitTests.FAADataParser.Utils
{
    [TestClass]
    public class TestParseLatitudeLongitude
    {
        [TestMethod]
        public void GoodLatitudeParsingTests()
        {
            List<(string, double)> dataList = new List<(string, double)>
            {
                (@"34-36-21.290N ", 34.60591389),
                (@"47-38-18.000N ", 47.63833333),
                (@"25-57-38.954S ", -25.96082056)
            };
            foreach ((string, double) elem in dataList)
            {
                Assert.IsTrue(ParseLatitudeLongitude.TryParse(elem.Item1, out double result));
                Assert.AreEqual(elem.Item2, result, 0.00000001);
            }
        }
        [TestMethod]
        public void GoodLongitudeParsingTests()
        {
            List<(string, double)> dataList = new List<(string, double)>
            {
                (@"134-36-21.290E", 134.60591389),
                (@"047-38-18.000E", 47.63833333),
                (@"125-57-38.954W", -125.96082056)
            };
            foreach ((string, double) elem in dataList)
            {
                Assert.IsTrue(ParseLatitudeLongitude.TryParse(elem.Item1, out double result));
                Assert.AreEqual(elem.Item2, result, 0.00000001);
            }
        }
    }
}
