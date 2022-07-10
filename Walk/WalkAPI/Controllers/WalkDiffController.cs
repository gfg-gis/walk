using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WalkAPI.Models.Domain;
using WalkAPI.Repositories;


namespace WalkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDiffController : Controller
    {
        private readonly IWalkDiffRepository walkDiffRepository;
        private readonly IMapper mapper;

        public WalkDiffController(IWalkDiffRepository walkDiffRepository, IMapper mapper)
        {
            this.walkDiffRepository = walkDiffRepository;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllWalkDiffAsync()
        {
            var walkDiff = await walkDiffRepository.GetAllAsync();

            var walkDiffDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDiff);

            return Ok(walkDiffDTO);
        }


        [HttpGet]
        [Route("{id:guid}")] // qui dinh phai truyen Guid, truyen gia tri khac la bao loi
        [ActionName("GetWalkDiffAsync")]  // cai nay dat ten giong route name cua Laravel 
        public async Task<IActionResult> GetWalkDiffAsync(Guid id)
        {
            var walkDiff = await walkDiffRepository.GetAsync(id);

            if (walkDiff == null) // co the bang null khi xai FirstOrDefaultAsync
            {
                return NotFound();
            }

            var walkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDiff);
            return Ok(walkDiffDTO);
        }


        [HttpPost]
        public async Task<IActionResult> AddWalkDiffAsync(Models.DTO.AddWalkDiffRequest addWalkRequest)
        {
            var walkDiff = new Models.Domain.WalkDifficulty()
            {
                
                Code = addWalkRequest.Code,
            
            };

            walkDiff = await walkDiffRepository.AddAsync(walkDiff);

            var walkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDiff);

            return CreatedAtAction(nameof(GetWalkDiffAsync), new { id = walkDiffDTO.Id }, walkDiffDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDiffAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateDiffRequest diffRequest)
        {
            var walkdiff = new Models.Domain.WalkDifficulty()
            {
                
                Code = diffRequest.Code,
            
            };

            walkdiff = await walkDiffRepository.UpdateAsync(id, walkdiff);


            if (walkdiff == null)
            {
                return NotFound();
            }

            var walkdiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkdiff);
            return Ok(walkdiffDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var walkDiff = await walkDiffRepository.DeleteAsync(id);

            if (walkDiff == null)
            {
                return NotFound();
            }

            var walkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDiff);
            return Ok(walkDiffDTO);
        }


    }
}
