using AviationApp.FAADataParser.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AviationApp.UnitTests.FAADataParser.Utils
{
    [TestClass]
    public class TestParsePhone
    {
        [TestMethod]
        public void TestAreaCodeNumberWithBrackets()
        {
            Assert.IsTrue(ParsePhone.TryParse(@"(425) 610-6293", out string parsed));
            Assert.AreEqual("+1 425-610-6293", parsed);
        }
        [TestMethod]
        public void TestFAA800Number()
        {
            Assert.IsTrue(ParsePhone.TryParse(@"8-772-2814", out string parsed));
            Assert.AreEqual("+1 800-772-2814", parsed);
        }
        [TestMethod]
        public void TestFaa1800Number()
        {
            Assert.IsTrue(ParsePhone.TryParse(@"1-292-5493", out string parsed));
            Assert.AreEqual("+1 800-292-5493", parsed);
        }
        [TestMethod]
        public void Test1800wxbrief()
        {
            Assert.IsTrue(ParsePhone.TryParse(@"1-800-WX-BRIEF", out string parsed));
            Assert.AreEqual("+1 800-992-7433", parsed);
        }
    }
}
