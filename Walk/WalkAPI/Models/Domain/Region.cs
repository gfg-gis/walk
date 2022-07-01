namespace WalkAPI.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public double Area { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public long Population { get; set; }

        // Navigation 
        // 1 region can have multi walk, quan he 1 nhieu
        public IEnumerable<Walk> Walks { get; set; } 
    }
}
