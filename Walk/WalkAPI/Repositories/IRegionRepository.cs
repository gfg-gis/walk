using WalkAPI.Models.Domain;

namespace WalkAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region>GetAsync(Guid id);

        
        Task<Region>AddRegionAsync(Region region);

        Task<Region>DeleteRegionAsync(Guid id);

        Task<Region>UpdateRegionAsync(Guid id, Region region);

    }
}
