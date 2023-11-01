using coreAPI.Models.Domain;

namespace coreAPI.Repositories.Walks
{
    public interface IWalksRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}