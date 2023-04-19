namespace NZWalks.API.Models.DTO;

public class WalkDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LengthInKM { get; set; }
    public string? WalkImageURL { get; set; }

    public DifficultyDto Difficulty { get; set; }
    public RegionDto Region { get; set; }
}