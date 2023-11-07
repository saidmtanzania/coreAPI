using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace coreAPI.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string readerRoleId = "e14f49d5-6c3f-4393-8610-a3c8cbdb618b";
            string writerRoleId = "8a6a959b-e451-4d82-ad31-7e2377d1041b";

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new() {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name ="Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new() {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name ="Writer",
                    NormalizedName = "Writer".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}