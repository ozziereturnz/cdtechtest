using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotSim;

namespace RobotSimTest
{
    [TestClass]
    public class RobotTest
    {
        private const int MAX_X = 4;
        private const int MAX_Y = 4;

        [DataTestMethod]
        [DataRow(0, 0, 0)] // Min values
        [DataRow(1, 3, 1)]
        [DataRow(2, 1, 2)]
        [DataRow(MAX_X, MAX_Y, 3)] // Max values
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
        [DataRow(MAX_X + 1, 0, 0)]
        [DataRow(0, -1, 0)]
        [DataRow(0, MAX_Y + 1, 0)]
        [DataRow(int.MinValue, 0, 0)]
        [DataRow(int.MaxValue, 0, 0)]
        [DataRow(0, int.MinValue, 0)]
        [DataRow(0, int.MaxValue, 0)]
        public void Place_OutOfBounds(int x, int y, Facing facing)
        {
            Robot robot = new Robot();
            robot.Place(x, y, facing);

            Assert.IsFalse(robot.Placed, "Robot is placed.");
            Assert.AreEqual(robot.X, default(int), "X position has changed.");
            Assert.AreEqual(robot.Y, default(int), "Y position has changed.");
            Assert.AreEqual(robot.Facing, default(Facing), "Facing has changed.");
        }

        public void Move_North()
        {
            int expected = 1;
            Robot robot = new Robot();
            robot.Place(0, 0, Facing.North);
            robot.Move();

            Assert.AreEqual(robot.X, 0, "Robot moved incorrectly.");
            Assert.AreEqual(robot.Y, expected, "Robot did not move North.");
        }

        public void Move_South()
        {
            int expected = 3;
            Robot robot = new Robot();
            robot.Place(3, MAX_Y, Facing.South);
            robot.Move();

            Assert.AreEqual(robot.X, 3, "Robot moved incorrectly.");
            Assert.AreEqual(robot.Y, expected, "Robot did not move South.");
        }

        public void Move_East()
        {
            int expected = 2;
            Robot robot = new Robot();
            robot.Place(1, 1, Facing.East);
            robot.Move();

            Assert.AreEqual(robot.X, expected, "Robot did not move East.");
            Assert.AreEqual(robot.Y, 1, "Robot moved incorrectly.");
        }

        public void Move_West()
        {
            int expected = 0;
            Robot robot = new Robot();
            robot.Place(1, MAX_Y, Facing.West);
            robot.Move();

            Assert.AreEqual(robot.X, expected, "Robot did not move West.");
            Assert.AreEqual(robot.Y, 4, "Robot moved incorrectly.");
        }

        public void Move_WithoutPlace()
        {
            Robot robot = new Robot();
            robot.Move();

            Assert.AreEqual(robot.X, default(int), "X position has changed.");
            Assert.AreEqual(robot.Y, default(int), "Y position has changed.");
        }

        [DataTestMethod]
        [DataRow(0, 0, Facing.West)]
        [DataRow(0, 0, Facing.South)]
        [DataRow(MAX_X, 0, Facing.East)]
        [DataRow(MAX_X, 0, Facing.South)]
        [DataRow(0, MAX_Y, Facing.West)]
        [DataRow(0, MAX_Y, Facing.North)]
        [DataRow(MAX_X, MAX_Y, Facing.East)]
        [DataRow(MAX_X, MAX_Y, Facing.North)]
        public void Move_OutOfBounds(int x, int y, Facing facing)
        {
            Robot robot = new Robot();
            robot.Place(x, y, facing);
            robot.Move();

            Assert.AreEqual(robot.X, x, "X position changed.");
            Assert.AreEqual(robot.Y, y, "Y position changed.");
        }
    }
}
