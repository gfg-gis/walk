using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalkAPI.Models.Domain;
using WalkAPI.Repositories;

namespace WalkAPI.Controllers
{
    [ApiController]

    // endpoint, cach 1 thong thuong
    // [Route("nz-regions")] 

    // end point, cach 2 dua tren ten cua class controller,
    // vi du RegionsController thi tu dong no se lay chu Regions
    [Route("[controller]")] 

    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
       public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllAsync();

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);

            // return DTO
            //var regionsDTO = new List<Models.DTO.Region>(); // mang chua 

            //regions.ToList().ForEach(region =>
            //{
            //    var regiondto = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Code,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,
            //    };

            //    regionsDTO.Add(regiondto);

            //});

        }
    }
}
