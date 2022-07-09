using Microsoft.EntityFrameworkCore;
using WalkAPI.Data;
using WalkAPI.Models.Domain;


namespace WalkAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalkDbContext mydbContext;

        public WalkRepository(WalkDbContext mydbContext)
        {
            this.mydbContext = mydbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await mydbContext.Walks.AddAsync(walk);
            await mydbContext.SaveChangesAsync();

            return walk;

        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await mydbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            mydbContext.Remove(existingWalk);
            await mydbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await mydbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await mydbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await mydbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await mydbContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
