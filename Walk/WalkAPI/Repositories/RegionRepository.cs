using Microsoft.EntityFrameworkCore;
using WalkAPI.Data;
using WalkAPI.Models.Domain;

namespace WalkAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalkDbContext mydbContext;

        public RegionRepository(WalkDbContext mydbContext)
        {
            this.mydbContext = mydbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await mydbContext.Regions.ToListAsync();
            
        }
    }
}
