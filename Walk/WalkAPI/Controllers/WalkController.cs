using Microsoft.AspNetCore.Mvc;
using AutoMapper;
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


    public class WalkController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            var walks = await walkRepository.GetAllAsync();

            var walkDTO = mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(walkDTO);
        }


        [HttpGet]
        [Route("{id:guid}")] // qui dinh phai truyen Guid, truyen gia tri khac la bao loi
        [ActionName("GetWalkAsync")]  // cai nay dat ten giong route name cua Laravel 
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await walkRepository.GetAsync(id);

            if (walk == null) // co the bang null khi xai FirstOrDefaultAsync
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId

            };

            walk = await walkRepository.AddAsync(walk);

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId

            };


            walk = await walkRepository.UpdateAsync(id, walk);


            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);


        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }


    }
}
