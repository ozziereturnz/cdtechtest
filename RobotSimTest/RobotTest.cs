using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using RobotSim;

namespace RobotSimTest
{
    [TestClass]
    public class RobotTest
    {
        private const int MAX_X = 4;
        private const int MAX_Y = 4;

        #region Place Tests
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
            Assert.AreEqual(x, robot.X, "X position is incorrect.");
            Assert.AreEqual(y, robot.Y, "Y position is incorrect.");
            Assert.AreEqual(facing, robot.Facing, "Facing is incorrect.");
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
            Assert.AreEqual(default(int), robot.X, "X position has changed.");
            Assert.AreEqual(default(int), robot.Y, "Y position has changed.");
            Assert.AreEqual(default(Facing), robot.Facing, "Facing has changed.");
        }

        [TestMethod]
        public void Place_Replace() {
            int expectedX = 2;
            int expectedY = 3;
            Facing expectedFacing = Facing.South;

            Robot robot = new Robot();
            robot.Place(0, 0, Facing.North);
            robot.Place(expectedX, expectedY, expectedFacing);
            
            Assert.IsTrue(robot.Placed, "Robot is not placed.");
            Assert.AreEqual(expectedX, robot.X, "X position is incorrect.");
            Assert.AreEqual(expectedY, robot.Y, "Y position is incorrect.");
            Assert.AreEqual(expectedFacing, robot.Facing, "Facing is incorrect.");
        }

        [TestMethod]
        public void Place_InvalidReplace() {
            int expectedX = 4;
            int expectedY = 1;
            Facing expectedFacing = Facing.West;

            Robot robot = new Robot();
            robot.Place(expectedX, expectedY, expectedFacing);
            robot.Place(int.MinValue, 0, Facing.North);
            
            Assert.IsTrue(robot.Placed, "Robot is not placed.");
            Assert.AreEqual(expectedX, robot.X, "X position is incorrect.");
            Assert.AreEqual(expectedY, robot.Y, "Y position is incorrect.");
            Assert.AreEqual(expectedFacing, robot.Facing, "Facing is incorrect.");
        }
        #endregion

        #region Move Tests
        [TestMethod]
        public void Move_North()
        {
            int expected = 1;
            Robot robot = new Robot();
            robot.Place(0, 0, Facing.North);
            robot.Move();

            Assert.AreEqual(0, robot.X, "Robot moved incorrectly.");
            Assert.AreEqual(expected, robot.Y, "Robot did not move North.");
        }

        [TestMethod]
        public void Move_South()
        {
            int expected = 3;
            Robot robot = new Robot();
            robot.Place(3, MAX_Y, Facing.South);
            robot.Move();

            Assert.AreEqual(3, robot.X, "Robot moved incorrectly.");
            Assert.AreEqual(expected, robot.Y, "Robot did not move South.");
        }

        [TestMethod]
        public void Move_East()
        {
            int expected = 2;
            Robot robot = new Robot();
            robot.Place(1, 1, Facing.East);
            robot.Move();

            Assert.AreEqual(expected, robot.X, "Robot did not move East.");
            Assert.AreEqual(1, robot.Y, "Robot moved incorrectly.");
        }

        [TestMethod]
        public void Move_West()
        {
            int expected = 0;
            Robot robot = new Robot();
            robot.Place(1, MAX_Y, Facing.West);
            robot.Move();

            Assert.AreEqual(expected, robot.X, "Robot did not move West.");
            Assert.AreEqual(4, robot.Y, "Robot moved incorrectly.");
        }

        [TestMethod]
        public void Move_WithoutPlace()
        {
            Robot robot = new Robot();
            robot.Move();

            Assert.AreEqual(default(int), robot.X, "X position has changed.");
            Assert.AreEqual(default(int), robot.Y, "Y position has changed.");
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

            Assert.AreEqual(x, robot.X, "X position changed.");
            Assert.AreEqual(y, robot.Y, "Y position changed.");
        }
        #endregion

        #region Left Tests
        [DataTestMethod]
        [DataRow(Facing.North, Facing.West)]
        [DataRow(Facing.West, Facing.South)]
        [DataRow(Facing.South, Facing.East)]
        [DataRow(Facing.East, Facing.North)]
        public void Left_Turns(Facing initial, Facing expected)
        {
            Robot robot = new Robot();
            robot.Place(0, 0, initial);
            robot.Left();

            Assert.AreEqual(expected, robot.Facing, "Robot facing wrong direction.");
        }

        [TestMethod]
        public void Left_NotPlaced()
        {
            Robot robot = new Robot();
            robot.Left();

            Assert.AreEqual(default(Facing), robot.Facing, "Robot has turned.");
        }
        #endregion
        
        #region Right Tests
        [DataTestMethod]
        [DataRow(Facing.North, Facing.East)]
        [DataRow(Facing.West, Facing.North)]
        [DataRow(Facing.South, Facing.West)]
        [DataRow(Facing.East, Facing.South)]
        public void Right_Turns(Facing initial, Facing expected)
        {
            Robot robot = new Robot();
            robot.Place(0, 0, initial);
            robot.Right();

            Assert.AreEqual(expected, robot.Facing, "Robot facing wrong direction.");
        }

        [TestMethod]
        public void Right_NotPlaced()
        {
            Robot robot = new Robot();
            robot.Right();

            Assert.AreEqual(default(Facing), robot.Facing, "Robot has turned.");
        }
        #endregion

        #region Report Tests
        [TestMethod]
        public void Report_IntialValues()
        {
            StringBuilder output = new StringBuilder();
            Console.SetOut(new StringWriter(output));

            int expectedX = 1;
            int expectedY = 2;
            Facing expectedFacing = Facing.South;

            Robot robot = new Robot();
            robot.Place(expectedX, expectedY, expectedFacing);
            robot.Report();

            Assert.IsTrue(output.ToString().Contains($"{expectedX}, {expectedY}, {expectedFacing}"), "Output does not contain correct report.");
        }

        [TestMethod]
        public void Report_Unplaced()
        {
            StringBuilder output = new StringBuilder();
            Console.SetOut(new StringWriter(output));

            Robot robot = new Robot();
            robot.Report();

            Assert.AreEqual(string.Empty, output.ToString(), "Output contains a report.");
        }

        [TestMethod]
        public void Report_AfterCommands()
        {
            StringBuilder output = new StringBuilder();
            Console.SetOut(new StringWriter(output));

            int expectedX = 2;
            int expectedY = 3;
            Facing expectedFacing = Facing.West;

            Robot robot = new Robot();
            robot.Place(0, 0, Facing.North);
            robot.Move();
            robot.Move();
            robot.Move();
            robot.Right();
            robot.Move();
            robot.Move();
            robot.Left();
            robot.Left();
            robot.Report();

            Assert.IsTrue(output.ToString().Contains($"{expectedX}, {expectedY}, {expectedFacing}"), "Output does not contain correct report.");
        }
        #endregion
    }
}
