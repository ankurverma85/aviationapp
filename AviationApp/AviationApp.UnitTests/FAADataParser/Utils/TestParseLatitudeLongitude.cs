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
            List<(string, decimal)> dataList = new List<(string, decimal)>
            {
                (@"34-36-21.290N ", 124581.290m),
                (@"47-38-18.000N ", 171498.000m),
                (@"25-57-38.954S ", -93458.954m),
                (@"140811.070N", 140811.070m)
            };
            foreach ((string, decimal) elem in dataList)
            {
                Assert.IsTrue(ParseLatitudeLongitude.TryParse(elem.Item1, out decimal result));
                Assert.AreEqual(elem.Item2, result);
            }
        }
        [TestMethod]
        public void GoodLongitudeParsingTests()
        {
            List<(string, decimal)> dataList = new List<(string, decimal)>
            {
                (@"134-36-21.290E", 484581.290m),
                (@"047-38-18.000E", 171498.000m),
                (@"125-57-38.954W", -453458.954m),
                (@"271674.660W", -271674.660m)
            };
            foreach ((string, decimal) elem in dataList)
            {
                Assert.IsTrue(ParseLatitudeLongitude.TryParse(elem.Item1, out decimal result));
                Assert.AreEqual(elem.Item2, result);
            }
        }
    }
}
