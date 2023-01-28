namespace RobotSim
{
    public class Robot
    {
        private const int MAX_X = 4;
        private const int MAX_Y = 4;

        public bool Placed { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Facing Facing { get; private set; }

        public void Place(int x, int y, Facing facing)
        {
            // TODO
        }

        public void Move()
        {
            // TODO
        }

        public void Left()
        {
            // TODO
        }

        public void Right()
        {
            // TODO
        }

        public Tuple<int, int, Facing> Report()
        {
            // TODO
            return new Tuple<int, int, Facing>(0, 0, 0);
        }
    }
}
