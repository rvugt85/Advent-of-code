using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AOCTests
{
    [TestClass]
    public class Day12Tests
    {
        public Day12 Day12 = new Day12();

        [TestMethod]
        public void FollowInstructionsTest()
        {
            var instructions = new List<string> { "F10", "N3", "F7", "R90", "F11" };

            var location = Day12.ProcessInstructions(instructions);

            Assert.AreEqual(-8, location.NorthSouth);
            Assert.AreEqual(17, location.EastWest);
        } 

        [TestMethod]
        public void FollowInstructionsWithWaypointTest()
        {
            var instructions = new List<string> { "F10", "N3", "F7", "R90", "F11" };

            var location = Day12.ProcessInstructionsWithWaypoint(instructions);

            Assert.AreEqual(-72, location.NorthSouth);
            Assert.AreEqual(214, location.EastWest);
        }
    }
}
