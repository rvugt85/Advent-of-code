using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOCTests
{
    [TestClass]
    public class Day24Tests
    {
        static string Part1Path = "\\AOCTests\\Day24Tests\\Day24TestInput.txt";
        
        private Day24 Day24 = new Day24();
        private MainProgram mainProgram = new MainProgram();

        [TestMethod]
        public void Part1Test()
        {
            var lines = mainProgram.ConvertFileToLines(Part1Path);

            var answer = Day24.GetBlackTiles(lines).Count;

            Assert.AreEqual(10, answer);
        }

        [TestMethod]
        public void Part2Test()
        {
            var lines = mainProgram.ConvertFileToLines(Part1Path);

            var coordinates = Day24.GetBlackTiles(lines);

            for(var days = 1; days <= 100; days++)
            {
                coordinates = Day24.RunRules(coordinates);
            }
            Assert.AreEqual(2208, coordinates.Count);
        }
    }
}
