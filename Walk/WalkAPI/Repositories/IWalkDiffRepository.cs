using WalkAPI.Models.Domain;

namespace WalkAPI.Repositories
{
    public interface IWalkDiffRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();

        Task<WalkDifficulty> GetAsync(Guid id);

        Task<WalkDifficulty> AddAsync(WalkDifficulty difficulty);

        Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty difficulty);

        Task<WalkDifficulty> DeleteAsync(Guid id);


    }
}
