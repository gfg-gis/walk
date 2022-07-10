using AutoMapper;

namespace WalkAPI.Profiles
{
    public class WalkDiffProfile : Profile 
    {
        public WalkDiffProfile()
        {
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>().ReverseMap();
        }
    }
}
