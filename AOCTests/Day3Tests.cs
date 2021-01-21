using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AOCTests
{
    [TestClass]
    public class Day3Tests
    {
        public List<string> lines = new List<string> { "..##.......", "#...#...#..", ".#....#..#.", "..#.#...#.#", ".#...##..#.", "..#.##.....", ".#.#.#....#", ".#........#", "#.##...#...", "#...##....#", ".#..#...#.#" };

        [TestMethod]
        [DataRow(1, 1, 2)]
        [DataRow(3, 1, 7)]
        [DataRow(5, 1, 3)]
        [DataRow(7, 1, 4)]
        [DataRow(1, 2, 2)]
        public void Day3Test(int right, int down, long answer)
        {
            var day3 = new Day3();

            var actual = day3.CalculateTrees(lines, right, down);

            Assert.AreEqual(answer, actual);
        }

        [TestMethod]
        public void Day3Part2Test()
        {
            var day3 = new Day3();

            var actual = day3.CalculateTreesPart2(lines);

            Assert.AreEqual(336, actual);
        }
    }
}
