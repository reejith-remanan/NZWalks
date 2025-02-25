using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]// https://localhost:1234/api/Difficulty
    [ApiController]// wii tell the application that the controller is for API use so it automatically validate the model state and gives a 400
                   // response back to the caller
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;
        private readonly IMapper mapper;

        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper mapper)
        {
            this.difficultyRepository = difficultyRepository;
            this.mapper = mapper;
        }
        //Get all difficulties
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll()
        {
            var difficultyModel = await difficultyRepository.GetAllAsync();

            var difficultyDto = mapper.Map<List<DifficultyDto>>(difficultyModel);

            return Ok(difficultyDto);
        }
    }
}
