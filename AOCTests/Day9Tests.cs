using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day9Tests
    {
        public Day9 Day9 = new Day9();

        public long[] LongArray = new long[]
            {
                35,20,15,25,47,40,62,55,65,95,102,117,150,182,127,219,299,277,309,576
            };

        [TestMethod]
        public void GetFirstInvalidNumberTest()
        {
            var invalidNumber = Day9.GetFirstInvalidNumber(LongArray, 5);

            Assert.AreEqual(127, invalidNumber);
        }

        [TestMethod]
        public void GetSumOfRangeTest()
        {
            var sum = Day9.GetFirstAndLastNumberOfContiguousRange(LongArray, 127);

            Assert.AreEqual(62, sum);
        }
    }
}
