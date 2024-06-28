using Microsoft.EntityFrameworkCore;

namespace ToyRobotChallenge.Service.Models
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Robot> Robots { get; set; }
    }
}
