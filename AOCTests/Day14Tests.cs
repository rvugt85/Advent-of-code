using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOCTests
{
    [TestClass]
    public class Day14Tests
    {
        public Day14 Day14 = new Day14();

        public List<string> Input = new List<string>
        {
            "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
            "mem[8] = 11",
            "mem[7] = 101",
            "mem[8] = 0"
        };





        [TestMethod]
        public void GetSumOfNumbersTest()
        {
            var sumOfNumbers = Day14.RunProgram(Input);

            Assert.AreEqual(165, sumOfNumbers);
        }

        [TestMethod]
        public void Version2Test()
        {
            var input = new List<string>
            {
                "mask = 000000000000000000000000000000X1001X",
                "mem[42] = 100",
                "mask = 00000000000000000000000000000000X0XX",
                "mem[26] = 1"
            };
            var sumOfNumbers = Day14.RunProgramVersie2(input);

            Assert.AreEqual(208, sumOfNumbers);
        }
    }
}


