using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOCTests
{
    [TestClass]
    public class Day13Tests
    {
        Day13 Day13 = new Day13();



        [TestMethod]
        public void Part1Test()
        {
            var calibrationData = new List<string> { "7",  "13", "59", "31", "19" };

            var answer = Day13.IdOfEarliestBusMultipliedByWaitingMinutes(939, calibrationData);

            Assert.AreEqual(295, answer);
        }

        [TestMethod]
        [DataRow(new string[] { "67", "7", "59", "61" }, 754018)]
        [DataRow(new string[] { "67", "x", "7", "59", "61" }, 779210)]
        [DataRow(new string[] { "67", "7", "x", "59", "61" }, 1261476)]
        [DataRow(new string[] { "1789", "37", "47", "1889" }, 1202161486)]
        public void Part2Test(string[] buses, long expectedAnswer)
        {
            var busNumbers = buses.ToList();

            var actualAnswer = Day13.CheckSpecificTimeOfBus(busNumbers);

            Assert.AreEqual(expectedAnswer, actualAnswer);
        }
    }
}
