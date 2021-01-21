using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day16Tests
    {
        
        private MainProgram mainProgram = new MainProgram();

        [TestMethod]
        public void CalculateScanningErrorRate()
        {
            var day16 = new Day16("\\AOCTests\\Day16Tests\\Day16TestInputPart1.txt");
            var rate = day16.RunDay16Part1();

            Assert.AreEqual(71, rate);
        }

        [TestMethod]
        public void ValidateFields()
        {
            var day16 = new Day16("\\AOCTests\\Day16Tests\\Day16TestInputPart2.txt");

            day16.RunDay16Part1();
            var fields = day16.RunDay16Part2("");

            Assert.AreEqual(12, fields["class"]);
            Assert.AreEqual(11, fields["row"]);
            Assert.AreEqual(13, fields["seat"]);

        }
    }
}
