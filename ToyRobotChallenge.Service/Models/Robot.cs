using System.Text.Json.Serialization;

namespace ToyRobotChallenge.Service.Models
{
    public class Robot
    {
        public int Id { get; set; }
        public int PositionX { get; private set; } = 0;
        public int PositionY { get; private set; } = 0;
        public Orientation Orientation { get; set; } = Orientation.North;

      
        private readonly GridConfiguration _configuration;

        public void Move(int x, int y, Orientation orientation)
        {
            PositionX = x;
            PositionY = y;
            Orientation = orientation;
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
