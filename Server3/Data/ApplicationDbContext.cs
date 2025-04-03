using Microsoft.EntityFrameworkCore;
using Server3.Models;


namespace Server3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Task> Task { get; set; }
        public DbSet<User> User { get; set; }
    }
}
