﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateDifficultyDto
    {
        [Required]
        public string Name { get; set; }
    }
}
