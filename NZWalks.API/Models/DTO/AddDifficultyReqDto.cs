using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddDifficultyReqDto
    {
        [Required]
        public string Name { get; set; }
    }
}
