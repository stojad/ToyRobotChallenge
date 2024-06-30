using System.Text.Json.Serialization;

namespace ToyRobotChallenge.Service.Models
{
    public class Robot
    {
        public int Id { get; set; }
        public int PositionX { get; private set; } = 0;
        public int PositionY { get; private set; } = 0;
        public Orientation Facing { get; private set; } = Orientation.North;
        public bool IsPlaced { get; private set; } = false;

        public string Report()
        {
            if (!IsPlaced)
                throw new InvalidOperationException("You must place the robot in an initial position using place() before using this command.");

            return $"{PositionX}, {PositionY}, {Facing.ToString().ToUpper()}";
        }

        public void Place(GridConfiguration configuration, int x, int y, Orientation facing)
        {
            if (x < 0 || x > configuration.GridSizeX - 1)
                throw new InvalidOperationException($"Value of x co-ordinate must be between 0 and {configuration.GridSizeX - 1}");

            if (y < 0 || y > configuration.GridSizeY - 1)
                throw new InvalidOperationException($"Value of y co-ordinate must be between 0 and {configuration.GridSizeY - 1}");

            PositionX = x;
            PositionY = y;
            Facing = facing;
            IsPlaced = true;
        }

        public void Move(GridConfiguration configuration)
        {
            if (!IsPlaced)
                throw new InvalidOperationException("You must place the robot in an initial position using place() before using this command.");

            if (
                Facing == Orientation.North && PositionY == configuration.GridSizeY - 1 ||
                Facing == Orientation.East && PositionX == configuration.GridSizeX - 1 ||
                Facing == Orientation.South && PositionY == 0 ||
                Facing == Orientation.West && PositionX == 0
               )
                throw new InvalidOperationException($"Cannot move robot {Facing} as its resultant position would be out of bounds.");

            if (Facing == Orientation.North)
                PositionY++;
            else if (Facing == Orientation.East)
                PositionX++;
            else if (Facing == Orientation.South)
                PositionY--;
            else
                PositionX--;
        }

        public void Left()
        {
            if (!IsPlaced)
                throw new InvalidOperationException("You must place the robot in an initial position using place() before using this command.");

            Facing = Facing switch
            {
                Orientation.North => Orientation.West,
                Orientation.East => Orientation.North,
                Orientation.South => Orientation.East,
                Orientation.West => Orientation.South
            };
        }

        public void Right()
        {
            if (!IsPlaced)
                throw new InvalidOperationException("You must place the robot in an initial position using place() before using this command.");

            Facing = Facing switch
            {
                Orientation.North => Orientation.East,
                Orientation.East => Orientation.South,
                Orientation.South => Orientation.West,
                Orientation.West => Orientation.North
            };
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Orientation
    {
        North,
        East,
        South,
        West
    }
}
