﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
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

        [HttpGet]
        [Route("GetById/{id}")]

        public async Task<IActionResult> GetByID(Guid id)
        {
            var difficulty = await difficultyRepository.GetByIdAsync(id);

            if (difficulty == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DifficultyDto>(difficulty));
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var difficulty = await difficultyRepository.DeleteAsync(id);

            if(difficulty == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DifficultyDto>(difficulty));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddDifficultyReqDto difficulty)
        {
            var difficultyModel = mapper.Map<Difficulty>(difficulty);

            var responseDifficulty = await difficultyRepository.CreateAsync(difficultyModel);

            return Ok(mapper.Map<AddDifficultyReqDto>(responseDifficulty));

        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> update([FromBody] UpdateDifficultyDto updateDifficultyDto, [FromRoute] Guid id)
        {
            var difficultyModel = mapper.Map<Difficulty>(updateDifficultyDto);


            var response = await difficultyRepository.UpdateAsync(difficultyModel, id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<DifficultyDto>(response));

        }

    }
}
