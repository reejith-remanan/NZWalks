using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;


        private readonly IMapper mapper;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        //Create walk
        //POST: /api/Walks 
        [HttpPost]
        [ValidateModel]// another way for validation using CustomActionFilter
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
                // Map DTO to Doamin model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);



                return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }




        //Get All Walks
        //Get: /api/Walks?filterON=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterquery,
                                                [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel =  await walkRepository.GetAllAsync(filterOn, filterquery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel)); 
        }




        //Get Walks by Id
        //Get: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walksDomainModel = await walkRepository.GetByIdAsync(id);

            if(walksDomainModel ==  null)
            {
                return NotFound();
            } 
            return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }




        //Update a Walk
        //Put: /api/Walks
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, UpdateWalkDto updateWalkDto)
        {
                var walksDomainModel = mapper.Map<Walk>(updateWalkDto);

                walksDomainModel = await walkRepository.UpdateAsync(id, walksDomainModel);

                if (walksDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(walksDomainModel));
        }





        //Delete a Walk
        //Delete /api/Walks
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer , Reader")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walksDomainModel = await walkRepository.DeleteAsync(id);
            if(walksDomainModel == null)
            {
                return NotFound();
            }

            return Ok("Record Deleted");
        }


    }
}
