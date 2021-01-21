using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day6Tests
    {
        public Day6 Day6 = new Day6();

        [TestMethod]
        [DataRow("abc",3)]
        [DataRow("a\r\nb\r\nc", 3)]
        [DataRow("ab\r\nac", 3)]
        [DataRow("a\r\na\r\na\r\na", 1)]
        [DataRow("b", 1)]
         public void TestGroupAnalysisAnyoneAnsweredYes(string group, int expectedValue)
        {
            Assert.AreEqual(expectedValue, Day6.CountofAnswersWithYesInGroup(group));
        }

        [TestMethod]
        [DataRow("abc", 3)]
        [DataRow("a\r\nb\r\nc", 0)]
        [DataRow("ab\r\nac", 1)]
        [DataRow("a\r\na\r\na\r\na", 1)]
        [DataRow("b", 1)]
        public void TestGroupAnalysisEveryoneAnsweredYes(string group, int expectedValue)
        {
            Assert.AreEqual(expectedValue, Day6.CountOfAnswersWithAllYesInGroup(group));
        }
    }
}
