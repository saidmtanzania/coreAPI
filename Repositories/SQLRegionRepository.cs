using coreAPI.Data;
using coreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace coreAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly CoreDbContext _dbContext;
        public SQLRegionRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _dbContext.Regions.FindAsync(id);
            if (existingRegion == null)
            {
                return null;
            }
            _dbContext.Regions.Remove(existingRegion);
            await _dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FindAsync(id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = _dbContext.Regions.Find(id);
            if (existingRegion == null)
            {
                return null;
            }

            //Update Region
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await _dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}