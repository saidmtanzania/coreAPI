using coreAPI.Data;
using coreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace coreAPI.Repositories.Walks
{
    public class SQLWalkRepository : IWalksRepository
    {
        private readonly CoreDbContext _dbContext;

        public SQLWalkRepository(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            //Adding new Created Walk to Database
            await _dbContext.Walks.AddAsync(walk);
            //Saving changes to database
            await _dbContext.SaveChangesAsync();
            //Returning created Walk response
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            //Fetching an existing Walk from database
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            //Checking if Walk exists if not return null
            if (existingWalk == null)
            {
                return null;
            }
            //Deleting Walk from database
            _dbContext.Walks.Remove(existingWalk);
            //Saving changes to database
            await _dbContext.SaveChangesAsync();
            //Returning deleted Walk response
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            //Getting all Walks from database  and return response
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            //Getting Walk from database by id and return response
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            //Fetching an existing Walks by Id from the database
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            //Checking if Walk exists if not return null
            if (existingWalk == null)
            {
                return null;
            }
            //For an existing Walk update
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            //Save changes to database
            await _dbContext.SaveChangesAsync();
            //Return updated Walk response
            return existingWalk;
        }
    }
}