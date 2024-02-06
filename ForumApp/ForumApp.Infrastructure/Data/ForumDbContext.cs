using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Infrastructure.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
