using Microsoft.EntityFrameworkCore;
using WalkAPI.Models.Domain;

namespace WalkAPI.Data
{
    public class WalkDbContext:DbContext
    {
        public WalkDbContext(DbContextOptions<WalkDbContext> options): base(options)
        {

        }

        // khai bao danh sách các table 
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}
