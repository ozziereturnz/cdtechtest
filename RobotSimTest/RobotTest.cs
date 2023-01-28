using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotSim;

namespace RobotSimTest
{
    [TestClass]
    public class RobotTest
    {
        [DataTestMethod]
        [DataRow(0, 0, 0)] // Min values
        [DataRow(1, 3, 1)]
        [DataRow(2, 1, 2)]
        [DataRow(4, 4, 3)] // Max values
        public void Place_ValidValues(int x, int y, Facing facing)
        {
            Robot robot = new Robot();
            robot.Place(x, y, facing);

            Assert.IsTrue(robot.Placed, "Robot is not placed.");
            Assert.AreEqual(robot.X, x, "X position is incorrect.");
            Assert.AreEqual(robot.Y, y, "Y position is incorrect.");
            Assert.AreEqual(robot.Facing, facing, "Facing is incorrect.");
        }

        [DataTestMethod]
        [DataRow(-1, 0, 0)]
        [DataRow(5, 0, 0)]
        [DataRow(0, -1, 0)]
        [DataRow(0, 5, 0)]
        [DataRow(int.MinValue, 0, 0)]
        [DataRow(int.MaxValue, 0, 0)]
        [DataRow(0, int.MinValue, 0)]
        [DataRow(0, int.MaxValue, 0)]
        public void Place_OutOfBounds(int x, int y, Facing facing)
        {
            Robot robot = new Robot();
            robot.Place(x, y, facing);

            Assert.IsFalse(robot.Placed, "Robot is placed.");
        }
    }
}
