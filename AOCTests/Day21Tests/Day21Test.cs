using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day21Test
    {
        public Day21 day21 = new Day21("\\AOCTests\\Day21Tests\\Day21TestInput.txt");

        [TestMethod]
        public void GetIngredientAppearance()
        {
            var answer = day21.GetIngredientAppearance();

            Assert.AreEqual(5, answer);
        }

        [TestMethod]
        public void GetCanonicalList()
        {
            var answer = day21.CreateCanonicalDangerousIngredientList();

            Assert.AreEqual("mxmxvkd,sqjhc,fvjkl", answer);
        }
    }
}
