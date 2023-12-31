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
            Walk? existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool? IsAsceding = true,
            int pageNumber = 1,
            int pageSize = 1000
        )
        {
            //Getting all Walks from database  and return response
            IQueryable<Walk> walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAsceding.HasValue && IsAsceding.Value ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);

                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAsceding.HasValue && IsAsceding.Value ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);

                }
            }

            //Pagination
            int skipResult = (pageNumber - 1) * pageSize;
            //
            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
            //_dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        private void elseif(bool v)
        {
            throw new NotImplementedException();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            //Getting Walk from database by id and return response
            return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            //Fetching an existing Walks by Id from the database
            Walk? existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
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