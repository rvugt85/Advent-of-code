using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day19Test
    {
        [TestMethod]
        public void CheckCorrectMessages()
        {
            var day19 = new Day19("\\AOCTests\\Day19Tests\\Day19TestInput.txt");

            var answer = day19.CheckMatchingMessages(0);

            Assert.AreEqual(2, answer);
        }

        [TestMethod]
        public void CheckCorrectMessagesWithLoop()
        {
            var day19 = new Day19("\\AOCTests\\Day19Tests\\Day19TestInputPart2.txt");

            var answer = day19.CheckMatchingMessagesFromTheMessage();

            Assert.AreEqual(12, answer);
        }


    }
}
