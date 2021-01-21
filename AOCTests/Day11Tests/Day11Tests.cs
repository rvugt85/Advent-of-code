using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day11Tests
    {
        public static string TestPath = "\\AOCTests\\Day11Tests\\Day11TestInput.txt";

        public Day11 Day11 = new Day11(TestPath);
        private MainProgram mainProgram = new MainProgram();

        [TestMethod]
        public void RunModelForArrivals()
        {
            var actualSeatsTaken = Day11.RunModelForArrivals();

            Assert.AreEqual(37, actualSeatsTaken);
        }

        [TestMethod]
        public void RunModelForArrivalsComplicatedSeatingRules()
        {
            var actualSeatsTaken = Day11.RunModelForArrivalsWithComplicateSeatingRules();

            Assert.AreEqual(26, actualSeatsTaken);
        }
    }
}
