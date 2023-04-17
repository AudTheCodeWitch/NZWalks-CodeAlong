namespace NZWalks.API.Models.Domain;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LengthInKM { get; set; }
    public string? WalkImageURL { get; set; } // ? means nullable

    public Guid DifficultyId { get; set; } // a walk has one difficulty
    public Guid RegionId { get; set; } // a walk has one region

    // Navigation properties
    // Navigation properties are used to navigate from one entity to another
    // They are associated with the foreign key of the related entity
    public Difficulty Difficulty { get; set; }
    public Region Region { get; set; }
}