using Microsoft.EntityFrameworkCore;

namespace CodeFirst.data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CodeFirst.Models.Student> Students { get; set; }
        public DbSet<CodeFirst.Models.Course> Courses { get; set; }

    }
}
