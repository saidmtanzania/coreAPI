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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Data Seeding for Difficulties
            //Easy, Medium, hard
            var difficulties = new List<Difficulty>()
            {
                new() { Id=Guid.Parse("beb25260-3c11-4bde-b638-d91b415710e6"), Name = "EASY",},
                new() { Id=Guid.Parse("ba8baf41-f687-42a1-adc0-883461b41cb7"), Name = "MEDIUM", },
                new() { Id=Guid.Parse("27ec554d-ef7c-49e2-ae6a-7f95205d7e40"), Name = "HARD", }
            };
            //Seeding Difficulties data to databases
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Data Seeding for Regions
            var regions = new List<Region>()
            {
                new() { Id=Guid.Parse("5636a3b1-e899-46e5-9fb3-a60a9e593fb7"), Code="TZ", Name="TANZANIA", RegionImageUrl="https://www.state.gov/wp-content/uploads/2018/11/Tanzania-e1555938157355-2501x1406.jpg"},
                new() { Id=Guid.Parse("97a6bd50-aa74-4dc5-b6de-1869db81d98f"), Code="KE", Name="KENYA", RegionImageUrl="https://destinationuganda.com/wp-content/uploads/2020/10/exploring-kampala-city-uganda-capital.jpg"},
                new() { Id=Guid.Parse("eb6fa6ba-f718-48f7-90a2-736da9197a70"), Code="UG", Name="UGANDA",  RegionImageUrl="https://a.travel-assets.com/findyours-php/viewfinder/images/res70/38000/38950-Nairobi.jpg"},
                new() { Id=Guid.Parse("64a4ef3b-0b1f-406c-81ff-25af1f1ea478"), Code="DZ", Name="ALGERIA",  RegionImageUrl="https://lp-cms-production.imgix.net/2023-10/iStock-985914532-RFC.jpg"},
                new() { Id=Guid.Parse("ed82e0ce-5945-41b3-9cbe-1ed90d23cb26"), Code="AO", Name="ANGOLA",  RegionImageUrl="https://unhabitat.org/sites/default/files/styles/featured_image_header_sm_focal/public/2019/05/shutterstock_1116891344.jpg"},
            };
            //Seeding Regions data to databases
            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}