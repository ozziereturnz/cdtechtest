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
            if ( x < 0 || x > MAX_X || y < 0 || y > MAX_Y) return; // Invalid placement, ignore.

            this.X = x;
            this.Y = y;
            this.Facing = facing;
            this.Placed = true;
        }

        public void Move()
        {
            if (!this.Placed) return; // Cannot move if not placed

            switch(this.Facing)
            {
                case Facing.North:
                {
                    this.Y += this.Y < MAX_Y ? 1 : 0;
                    break;
                }
                case Facing.South:
                {
                    this.Y -= this.Y > 0 ? 1 : 0;
                    break;
                }
                case Facing.East:
                {
                    this.X += this.X < MAX_X ? 1 : 0;
                    break;
                }
                case Facing.West:
                {
                    this.X -= this.X > 0 ? 1 : 0;
                    break;
                }
            }
        }

        public void Left()
        {
            if (!this.Placed) return; // Cannot turn if not placed

            if (this.Facing == Facing.North) this.Facing = Facing.West;
            else this.Facing--;
        }

        public void Right()
        {
            if (!this.Placed) return; // Cannot turn if not placed

            if (this.Facing == Facing.West) this.Facing = Facing.North;
            else this.Facing++;
        }

        public void Report()
        {
            // TODO
        }
    }
}
