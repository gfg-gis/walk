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

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid(); // Guid do minh chu dong tao
            await mydbContext.AddAsync(region);
            await mydbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region = await mydbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }

            mydbContext.Regions.Remove(region);
            await mydbContext.SaveChangesAsync();
            return region;

        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await mydbContext.Regions.ToListAsync();
            
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await mydbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await mydbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await mydbContext.SaveChangesAsync();

            return existingRegion;

        }
    }
}
