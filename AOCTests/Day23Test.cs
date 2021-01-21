using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day23Test
    {
        [TestMethod]
        [DataRow(10, "92658374")]
        [DataRow(100,  "67384529")]
        //[DataRow(10000000, true, "149245887792")]
        public void GetFinalConfiguration(int roundCount, string expectedResult)
        {
            var day23 = new Day23();

            var cups = day23.PlayRounds("389125467", 9, roundCount);

            var actualResult = day23.GetCupOrderTotal(cups);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [DataRow(10000000, 149245887792)]
        //[DataRow(10000000, true, "149245887792")]
        public void GetMultiplicationOfTwoNeighbors(int roundCount, long expectedResult)
        {
            var day23 = new Day23();

            var cups = day23.PlayRounds("389125467", 1000000, roundCount);

            var neighbor = cups[1];
            var neighbor2 = cups[neighbor];

            long actualResult = (long)cups[1] * (long)cups[cups[1]];

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
