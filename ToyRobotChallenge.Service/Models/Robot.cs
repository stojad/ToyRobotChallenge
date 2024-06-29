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

        public string Report() => $"{PositionX}, {PositionY}, {Facing.ToString().ToUpper()}";

        public void Place(int x, int y, Orientation facing)
        {
            PositionX = x;
            PositionY = y;
            Facing = facing;
            IsPlaced = true;
        }

        public void Move()
        {
            if (Facing == Orientation.North)
                PositionY++;
            else if (Facing == Orientation.East)
                PositionX++;
            else if (Facing == Orientation.South)
                PositionY--;
            else
                PositionX--;
        }

        public void Left() => Facing = Facing switch
        {
            Orientation.North => Orientation.West,
            Orientation.East => Orientation.North,
            Orientation.South => Orientation.East,
            Orientation.West => Orientation.South
        };

        public void Right() => Facing = Facing switch
        {
            Orientation.North => Orientation.East,
            Orientation.East => Orientation.South,
            Orientation.South => Orientation.West,
            Orientation.West => Orientation.North
        };
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
