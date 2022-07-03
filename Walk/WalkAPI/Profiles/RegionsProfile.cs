using AutoMapper;
namespace WalkAPI.Profiles
{
    public class RegionsProfile: Profile 
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();

        }
    }
}
