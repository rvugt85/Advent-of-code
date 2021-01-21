using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day7Tests
    {
        public string Path = "\\AOCTests\\Day7Tests\\Day7TestInput.txt";

        public Day7 Day7 = new Day7();
        public MainProgram mainProgram = new MainProgram();

        [TestMethod]
         public void Part1Test()
        {
            var colorAsked = "shiny gold";
            var rules = mainProgram.ConvertFileToLines(Path);

            Assert.AreEqual(4, Day7.GetCountOfPossibleColors(rules, colorAsked));
        }

        [TestMethod]
        [DataRow("\\AOCTests\\Day7Tests\\Day7TestInput.txt", 32)]
        [DataRow("\\AOCTests\\Day7Tests\\Day7TestInput2.txt", 126)]
        public void Part2Test(string path, int result)
        {
            var colorAsked = "shiny gold";
            var rules = mainProgram.ConvertFileToLines(path);

            Assert.AreEqual(result, Day7.GetCountOfBagsNeeded(rules, colorAsked));
        }
    }
}
