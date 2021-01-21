using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCTests
{
    [TestClass]
    public class Day25Test
    {
        private Day25 Day25 = new Day25();

        [TestMethod]
        [DataRow(17807724, 11)]
        [DataRow(5764801, 8)]
        public void GetLoopSize(int publicKey, int expectedLoopsize)
        {
            var actualLoopsize = Day25.FindLoopSize(publicKey);

            Assert.AreEqual(expectedLoopsize, actualLoopsize);
        }

        [TestMethod]
        [DataRow(17807724, 8, 14897079)]
        [DataRow(5764801, 11, 14897079)]
        public void GetEncryptionKey(int publicKey, int loopSize, int encryptionKey)
        {
            var day25 = new Day25();

            var actualEncryptionKey = day25.GetEncryptionKey(publicKey, loopSize);

            Assert.AreEqual(encryptionKey, actualEncryptionKey);
        }

       
    }
}
