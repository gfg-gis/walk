using Microsoft.EntityFrameworkCore;
using WalkAPI.Data;
using WalkAPI.Models.Domain;

namespace WalkAPI.Repositories
{
    public class WalkDiffRepository : IWalkDiffRepository
    {
        private WalkDbContext mydbContext;

        public WalkDiffRepository(WalkDbContext mydbContext)
        {
          
                this.mydbContext = mydbContext;
          
        }

       

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await mydbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await mydbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
           

        }


        public async Task<WalkDifficulty> AddAsync(WalkDifficulty difficulty)
        {
            difficulty.Id = Guid.NewGuid(); // Guid do minh chu dong tao
            await mydbContext.AddAsync(difficulty);
            await mydbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty difficulty)
        {
            var existingDiff = await mydbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDiff == null)
            {
                return null;
            }

            existingDiff.Code = difficulty.Code;
            await mydbContext.SaveChangesAsync();
            return existingDiff;

        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDiff = await mydbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDiff == null)
            {
                return null;
            }

            mydbContext.WalkDifficulty.Remove(walkDiff);
            await mydbContext.SaveChangesAsync();
            return walkDiff;
        }
    }
}
