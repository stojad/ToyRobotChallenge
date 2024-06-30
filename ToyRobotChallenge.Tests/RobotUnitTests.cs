using ToyRobotChallenge.Service.Models;

namespace ToyRobotChallenge.Tests
{
    public class RobotUnitTests
    {
        private readonly GridConfiguration _configuration = new GridConfiguration { GridSizeX = 5, GridSizeY = 5 };
        
        [Fact]
        public void Report_WhenIsNotPlaced_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new Robot().Report());
        }

        [Fact]
        public void Move_WhenIsNotPlaced_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new Robot().Move(_configuration));
        }

        [Fact]
        public void Left_WhenIsNotPlaced_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new Robot().Left());
        }

        [Fact]
        public void Right_WhenIsNotPlaced_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => new Robot().Right());
        }

        [Theory]
        [InlineData(0, 0, Orientation.North)]
        [InlineData(1, 2, Orientation.East)]
        [InlineData(2, 1, Orientation.South)]
        [InlineData(3, 3, Orientation.West)]
        public void Report_WhenIsPlaced_ShowsCorrectState(int x, int y, Orientation facing)
        {
            var robot = new Robot();
            robot.Place(_configuration, x, y, facing);
            Assert.Equal($"{x}, {y}, {facing.ToString().ToUpper()}", robot.Report());
        }

        [Theory]
        [InlineData(-1, 0, Orientation.North)]
        [InlineData(0, -1, Orientation.East)]
        [InlineData(0, 5, Orientation.South)]
        [InlineData(5, 0, Orientation.West)]
        public void Place_WhenOutOfBounds_ThrowsException(int x, int y, Orientation facing)
        {
            Assert.Throws<InvalidOperationException>(() => new Robot().Place(_configuration, x, y, facing));
        }

        [Theory]
        [InlineData(Orientation.North, Orientation.East)]
        [InlineData(Orientation.East, Orientation.South)]
        [InlineData(Orientation.South, Orientation.West)]
        [InlineData(Orientation.West, Orientation.North)]
        public void Right_Changes_Orientation_Correctly(Orientation before, Orientation after)
        {
            var robot = new Robot();
            robot.Place(_configuration, 0, 0, before);
            robot.Right();
            Assert.Equal(after, robot.Facing);
        }

        [Theory]
        [InlineData(Orientation.North, Orientation.West)]
        [InlineData(Orientation.East, Orientation.North)]
        [InlineData(Orientation.South, Orientation.East)]
        [InlineData(Orientation.West, Orientation.South)]
        public void Left_Changes_Orientation_Correctly(Orientation before, Orientation after)
        {
            var robot = new Robot();
            robot.Place(_configuration, 0, 0, before);
            robot.Left();
            Assert.Equal(after, robot.Facing);
        }

        [Theory]
        [InlineData(Orientation.North, 1, 2)]
        [InlineData(Orientation.East, 2, 1)]
        [InlineData(Orientation.South, 1, 0)]
        [InlineData(Orientation.West, 0, 1)]
        public void Move_WhenInBounds_ChangesCorrectCoordinate(Orientation facing, int expectedX, int expectedY)
        {
            var robot = new Robot();
            robot.Place(_configuration, 1, 1, facing);
            robot.Move(_configuration);
            Assert.Equal(facing, robot.Facing);
            Assert.Equal(expectedX, robot.PositionX);
            Assert.Equal(expectedY, robot.PositionY);
        }

        [Theory]
        [InlineData(3, 4, Orientation.North)]
        [InlineData(4, 3, Orientation.East)]
        [InlineData(1, 0, Orientation.South)]
        [InlineData(0, 1, Orientation.West)]
        public void Move_WhenOutOfBounds_ThrowsException(int x, int y, Orientation facing)
        {
            var robot = new Robot();
            robot.Place(_configuration, x, y, facing);
            Assert.Throws<InvalidOperationException>(() => robot.Move(_configuration));
        }
    }
}
