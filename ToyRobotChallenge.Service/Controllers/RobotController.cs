using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
                ?? throw new NullReferenceException("Failed to retrieve grid configuration");

            if (!_context.Robots.Any())
            {
                _context.Robots.Add(new Robot());
                _context.SaveChanges();
            }
        }

        [HttpGet()]
        [Route("report")]
        public Robot Report()
        {
            return _context.Robots.Single();
        }

        [HttpPut()]
        [Route("move")]
        public IActionResult Move(int x, int y, Orientation facing)
        {
            try
            {
                if (x < 0 || x > _configuration.GridSizeX - 1)
                    throw new InvalidOperationException($"Value of x co-ordinate must be between 0 and {_configuration.GridSizeX - 1}");

                if (y < 0 || y > _configuration.GridSizeY - 1)
                    throw new InvalidOperationException($"Value of y co-ordinate must be between 0 and {_configuration.GridSizeY - 1}");

                _context.Robots.Single().Move(x, y, facing);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
