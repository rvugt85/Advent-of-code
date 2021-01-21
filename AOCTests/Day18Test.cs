using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AOCTests
{
    [TestClass]
    public class Day18Test
    {
        public Day18 Day18 = new Day18();

        [TestMethod]
        [DataRow("2 * 3 + (4 * 5)", 26)]
        [DataRow("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
        [DataRow("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
        [DataRow("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
        public void GetEasySumResults(string input, int expectedResult)
        {
            var lineWithoutSpaces = Regex.Replace(input, @"\s+", "");

            var actualResult = Day18.GetSumResultEasy(lineWithoutSpaces);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [DataRow("1 + (2 * 3) + (4 * (5 + 6))", 51)]
        [DataRow("2 * 3 + (4 * 5)", 46)]
        [DataRow("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
        [DataRow("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
        [DataRow("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
        public void GetHardSumResults(string input, int expectedResult)
        {
            var lineWithoutSpaces = Regex.Replace(input, @"\s+", "");

            var actualResult = Day18.GetSumResultHard(lineWithoutSpaces, 0);

            Assert.AreEqual(expectedResult, actualResult);
        }


    }
}
