using coreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace coreAPI.Data
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}