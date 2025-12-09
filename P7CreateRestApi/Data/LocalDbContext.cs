using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;

namespace Dot.Net.WebApi.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Rule> Rules { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
    }
}