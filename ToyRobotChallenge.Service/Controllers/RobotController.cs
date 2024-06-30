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
            try
            {
                return Content(_context.Robots.Single().Report());
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [Route("place")]
        public IActionResult Place(int x, int y, Orientation facing)
        {
            try
            {
                _context.Robots.Single().Place(_configuration, x, y, facing);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpPut()]
        [Route("move")]
        public IActionResult Move()
        {
            try
            {
                _context.Robots.Single().Move(_configuration);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpPut()]
        [Route("left")]
        public IActionResult Left()
        {
            try
            {
                _context.Robots.Single().Left();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            _context.SaveChanges();
            return Ok();
        }

        [HttpPut()]
        [Route("right")]
        public IActionResult Right()
        {
            try
            {
                _context.Robots.Single().Right();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
