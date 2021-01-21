using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOCTests
{
    [TestClass]
    public class Day5Tests
    {
        public Day5 Day5 = new Day5();

        [TestMethod]
        [DataRow("1000110", 70)]
        [DataRow("0001110", 14)]
        [DataRow("1100110", 102)]
        [DataRow("111", 7)]
        [DataRow("100", 4)]
        [DataRow("001", 1)]
        public void TestCreateNumber(string binaryCode, int number)
        {
            Assert.AreEqual(number, Day5.CreateNumber(binaryCode));
        }

        [TestMethod]
        [DataRow("BFFFBBFRRR", 567)]
        [DataRow("FFFBBBFRRR", 119)]
        [DataRow("BBFFBBFRLL", 820)]
        public void TestCreateSeatId(string seatcode, int seatId)
        {
            Assert.AreEqual(seatId, Day5.CreateSeatId(seatcode));
        }

        [TestMethod]
        public void TestGetHighestSeatId()
        {
            var seatIds = new List<string> { "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL" };
            var actual = Day5.GetHighestSeatId(seatIds);

            Assert.AreEqual(820, actual);
        }

    }
}
