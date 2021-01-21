using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AOCTests
{
    [TestClass]
    public class Day10Tests
    {
        public Day10 Day10 = new Day10();

        public List<int> IntList1 = new List<int>
            {
                1,2 ,3 ,4 ,7 ,8 ,9 ,10,11,14,17,18,19,20,23,24,25,28,31,32,33,34,35,38,39,42,45,46,47,48,49, 52
            };

        public List<int> IntList2 = new List<int>
           {
            1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, 22
        };











        [TestMethod]
        public void GetCombinationBetween1And3JoltDifferencesTest()
        {
            var combination = Day10.GetCombinationBetween1And3JoltDifferences(IntList1);

            Assert.AreEqual(220, combination);
        }

        [TestMethod]
        public void GetDifferentWaysForAdaptersToArrangeList1()
        {
            var ways = Day10.GetPossibleWayToArrange(IntList1, -1, 0, 0);

            Assert.AreEqual(19208, ways);
        }

        [TestMethod]
        public void GetDifferentWaysForAdaptersToArrangeList2()
        {
            var ways = Day10.GetPossibleWayToArrange(IntList2, -1, 0, 0);

            Assert.AreEqual(8, ways);
        }


    }
}
