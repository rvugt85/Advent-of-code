using Microsoft.VisualStudio.TestTools.UnitTesting;
using AOC_2020;

namespace AOCTests
{
    [TestClass]
    public class Day2Tests
    {
        [TestMethod]
        [DataRow("1-3 a: abcde", true)]
        [DataRow("1-3 b: cdefg", false)]
        [DataRow("2-9 c: ccccccccc", true)]
        public void Day2Part1Tests(string input, bool expected)
        {
            var day2 = new Day2();

            var actual = day2.CheckPassword(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DataRow("1-3 a: abcde", true)]
        [DataRow("1-3 b: cdefg", false)]
        [DataRow("2-9 c: ccccccccc", false)]
        public void Day2Part2Tests(string input, bool expected)
        {
            var day2 = new Day2();

            var actual = day2.CheckPassword2(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
