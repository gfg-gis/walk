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
       public async Task<IActionResult> GetAllRegionsAsync()
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

        [HttpGet]
        [Route("{id:guid}")] // qui dinh phai truyen Guid, truyen gia tri khac la bao loi
        [ActionName("GetRegionAsync")]  // cai nay dat ten giong route name cua Laravel 
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null) // co the bang null khi xai FirstOrDefaultAsync
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };

            region = await regionRepository.AddRegionAsync(region);

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            var region = await regionRepository.DeleteRegionAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest regionRequest)
        {
            var region = new Models.Domain.Region()
            {
                Name = regionRequest.Name,
                Code = regionRequest.Code,
                Area = regionRequest.Area,
                Lat = regionRequest.Lat,
                Long = regionRequest.Long,
                Population = regionRequest.Population,
            };

            region = await regionRepository.UpdateRegionAsync(id, region);


            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);

        }


    }
}
