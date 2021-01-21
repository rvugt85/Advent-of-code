using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day17Test
    {
        public Day17 Day17 = new Day17("\\AOCTests\\Day17Tests\\Day17TestInput.txt");

        [TestMethod]
        public void RunCyclesOnThreeDimensions()
        {
            var answer = Day17.RunDay17Part1();

            Assert.AreEqual(112, answer);
        }

        [TestMethod]
        public void RunCyclesOnFourDimensions()
        {
            var answer = Day17.RunDay17Part2();

            Assert.AreEqual(848, answer);
        }


    }
}
