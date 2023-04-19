using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO;

public class UpdateWalkRequestDto
{
    [Required] public Guid Id { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string Name { get; set; }

    [Required]
    [MaxLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
    public string Description { get; set; }

    [Required]
    [Range(0, 50, ErrorMessage = "LengthInKM must be between 0 and 50")]
    public string LengthInKM { get; set; }

    public string? WalkImageURL { get; set; }

    [Required] public DifficultyDto Difficulty { get; set; }
    [Required] public RegionDto Region { get; set; }
}