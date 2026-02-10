using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Entities;

namespace P7CreateRestApi.Data
{
    public class LocalDbContext : IdentityDbContext<ApplicationUser>
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options)
            : base(options) { }

        public DbSet<BidList> BidLists { get; set; } = null!;
        public DbSet<CurvePoint> CurvePoints { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Rule> Rules { get; set; } = null!;
        public DbSet<Trade> Trades { get; set; } = null!;
    }
}
