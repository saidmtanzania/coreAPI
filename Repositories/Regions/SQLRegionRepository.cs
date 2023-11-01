using coreAPI.Data;
using coreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace coreAPI.Repositories.Regions
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
            //Add new created Region to the database
            await _dbContext.Regions.AddAsync(region);
            //Save created region Changes to the database
            await _dbContext.SaveChangesAsync();
            //Return created region response
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            //fetch an existing region from the database
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //Check if region exist in the database
            if (existingRegion == null)
            {
                return null;
            }
            //Delete an existing Region from the database
            _dbContext.Regions.Remove(existingRegion);
            //Save deleted region Changes to the database
            await _dbContext.SaveChangesAsync();
            //Return deleted region response
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            //fetch all regions from database and return response
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            //Getting a region by ID and return response
            return await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            //fetch an existing region from the databases
            var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //Check if region exist in the database
            if (existingRegion == null)
            {
                return null;
            }
            //Update an existing Region
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            //Save updated region Changes to the database
            await _dbContext.SaveChangesAsync();
            //Return updated region response
            return existingRegion;
        }
    }
}