using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOCTests
{
    [TestClass]
    public class Day15Test
    {
        public Day15 Day15 = new Day15();

        [TestMethod]
        [DataRow("1,3,2", 2020, 1)]
        [DataRow("2,1,3", 2020, 10)]
        [DataRow("1,2,3", 2020, 27)]
        [DataRow("2,3,1", 2020, 78)]
        [DataRow("3,2,1", 2020, 438)]
        [DataRow("3,1,2", 2020, 1836)]
        [DataRow("0,3,6", 2020, 436)]
        [DataRow("1,3,2", 30000000, 2578)]
        [DataRow("2,1,3", 30000000, 3544142)]
        [DataRow("1,2,3", 30000000, 261214)]
        [DataRow("2,3,1", 30000000, 6895259)]
        [DataRow("3,2,1", 30000000, 18)]
        [DataRow("3,1,2", 30000000, 362)]
        [DataRow("0,3,6", 30000000, 175594)]
        public void GetSpecificNumberTest(string input, int number, int expectedResult)
        {
            var splitInput = input.Split(',');
            int[] inputNumbers = new int[splitInput.Length];

            for(int i = 0; i<inputNumbers.Length;i++)
            {
                inputNumbers[i] = int.Parse(splitInput[i]);
            }
            var actualResult = Day15.FindSpecificNumber(inputNumbers, number);

            Assert.AreEqual(expectedResult, actualResult);
        }


    }
}
