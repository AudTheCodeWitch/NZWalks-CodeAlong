using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")] // api/Regions
[ApiController]
public class RegionsController : Controller
{
    private readonly NZWalksDbContext _dbContext;

    public RegionsController(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET all regions
    // GET: https://localhost:5001/api/Regions
    [HttpGet]
    public IActionResult GetAll()
    {
        // Get all regions from the database - domain models
        var regions = _dbContext.Regions.ToList();

        // Map domain models to DTOs
        var regionsDto = regions.Select(region => new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageURL
        }).ToList();

        return Ok(regionsDto);
    }

    // GET a region by id
    // GET: https://localhost:5001/api/Regions/1
    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        // Get a region domain model from the database
        var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

        // If the region is null, return a 404
        if (region == null) return NotFound();

        // Map the domain model to a DTO
        var regionDto = new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageURL
        };

        return Ok(regionDto);
    }
}