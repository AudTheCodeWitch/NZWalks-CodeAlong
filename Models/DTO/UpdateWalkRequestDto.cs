namespace NZWalks.API.Models.DTO;

public class UpdateWalkRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string LengthInKM { get; set; }
    public string? WalkImageURL { get; set; }

    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
}