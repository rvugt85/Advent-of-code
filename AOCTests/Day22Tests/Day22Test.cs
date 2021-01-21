using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day22Test
    {
        [TestMethod]
        public void GetWinnerScoreInRecursiveGame()
        {
            var day22 = new Day22("\\AOCTests\\Day22Tests\\Day22TestInput.txt");

            day22.RunDay22Part2();

            //Assert.AreEqual(291, score);
        }

        //[TestMethod]
        //public void GetNotSeaMonster()
        //{
        //    var day20 = new Day20("\\AOCTests\\Day20Tests\\Day20TestInput.txt");

        //    var answer = day20.GetNotSeaMonster();

        //    Assert.AreEqual(273, answer);
        //}

        //[TestMethod]
        //public void GetFullPicture()
        //{
        //    var day20 = new Day20("\\AOCTests\\Day20Tests\\Day20TestInput.txt");

        //    var picture = day20.CreateFullPicture();
        //    var finalArray = day20.FlipHorizontally(picture);

        //    var mainProgram = new MainProgram();
        //    var lines = mainProgram.ConvertFileToLines("\\AOCTests\\Day20Tests\\Day20TestResult.txt");
        //    var array = mainProgram.ConvertLinesToTwoDimensionalCharArray(lines);

        //    for (int x = 0; x < picture.GetLength(0); x++)
        //        for (int y = picture.GetLength(0) - 1; y >= 0; y--)
        //        {
        //            Assert.AreEqual(array[x, y], finalArray[x, y], $"Coordinate x = {0} and y = {y} is wrong");
        //        }
        //}

    }
}
