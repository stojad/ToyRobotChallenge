using Microsoft.AspNetCore.Mvc;
using ToyRobotChallenge.Service.Models;

namespace ToyRobotChallenge.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RobotController : Controller
    {
        private readonly ServiceDbContext _context;
        private readonly GridConfiguration _configuration;
        public RobotController(IConfiguration configuration, ServiceDbContext context)
        {
            _context = context;
            _configuration = configuration.GetSection(GridConfiguration.Key)?.Get<GridConfiguration>()
                ?? throw new NullReferenceException("Failed to retrieve grid configuration.");

            if (!_context.Robots.Any())
            {
                _context.Robots.Add(new Robot());
                _context.SaveChanges();
            }
        }

        [HttpGet()]
        [Route("report")]
        public IActionResult Report()
        {
            var robot = _context.Robots.Single();

            if (!robot.IsPlaced)
                return BadRequest("You must place the robot in an initial position using place() before using this command.");

            return Content(robot.Report());
        }

        [HttpPut()]
        [Route("place")]
        public IActionResult Place(int x, int y, Orientation facing)
        {
            if (x < 0 || x > _configuration.GridSizeX - 1)
                return BadRequest($"Value of x co-ordinate must be between 0 and {_configuration.GridSizeX - 1}");

            if (y < 0 || y > _configuration.GridSizeY - 1)
                return BadRequest($"Value of y co-ordinate must be between 0 and {_configuration.GridSizeY - 1}");

            _context.Robots.Single().Place(x, y, facing);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut()]
        [Route("move")]
        public IActionResult Move()
        {
            var robot = _context.Robots.Single();

            if (!robot.IsPlaced)
                return BadRequest("You must place the robot in an initial position using place() before using this command.");

            if (
                robot.Facing == Orientation.North && robot.PositionY == _configuration.GridSizeY - 1 ||
                robot.Facing == Orientation.East && robot.PositionX == _configuration.GridSizeX - 1 ||
                robot.Facing == Orientation.South && robot.PositionY == 0 ||
                robot.Facing == Orientation.West && robot.PositionX == 0
               )
                return BadRequest($"Cannot move robot {robot.Facing} as its resultant position would be out of bounds.");

            robot.Move();
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut()]
        [Route("left")]
        public IActionResult Left()
        {
            var robot = _context.Robots.Single();

            if (!robot.IsPlaced)
                return BadRequest("You must place the robot in an initial position using place() before using this command.");

            robot.Left();
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut()]
        [Route("right")]
        public IActionResult Right()
        {
            var robot = _context.Robots.Single();

            if (!robot.IsPlaced)
                return BadRequest("You must place the robot in an initial position using place() before using this command.");

            robot.Right();
            _context.SaveChanges();

            return Ok();
        }
    }
}
