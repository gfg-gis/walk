using WalkAPI.Models.Domain;

namespace WalkAPI.Models.DTO
{
    public class Walk
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }

        // Navigation 
        // 1 Walk chi thuoc ve 1 region 
        public Region Region { get; set; }
        // 1 Walk chi thuoc ve 1 do kho 
        public WalkDifficulty WalkDifficulty { get; set; }
    }
}
