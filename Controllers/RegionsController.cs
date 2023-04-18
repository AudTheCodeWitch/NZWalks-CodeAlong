using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
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
    // GET: https://localhost:portnumber/api/Regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get all regions from the database - domain models
        var regions = await _dbContext.Regions.ToListAsync();

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
    // GET: https://localhost:portnumber/api/Regions/1
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        // Get a region domain model from the database
        var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

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

    // Create a new region
    // POST: https://localhost:portnumber/api/Regions
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto request)
    {
        // Map or convert the DTO to a domain model
        var regionDomainModel = new Region
        {
            Id = Guid.NewGuid(),
            Code = request.Code,
            Name = request.Name,
            RegionImageURL = request.RegionImageUrl
        };

        // Add the region to the database
        await _dbContext.Regions.AddAsync(regionDomainModel);
        await _dbContext.SaveChangesAsync();

        // Map the domain model back to a DTO
        var regionDto = new RegionDto
        {
            Id = regionDomainModel.Id,
            Code = regionDomainModel.Code,
            Name = regionDomainModel.Name,
            RegionImageUrl = regionDomainModel.RegionImageURL
        };

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // Update a region
    // PUT: https://localhost:portnumber/api/Regions/1
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto request)
    {
        // Get the region from the database
        var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        // If the region is null, return a 404
        if (region == null) return NotFound();

        // Update the region
        region.Code = request.Code;
        region.Name = request.Name;
        region.RegionImageURL = request.RegionImageUrl;

        // Save the changes to the database
        await _dbContext.SaveChangesAsync();

        // Map the domain model back to a DTO
        var regionDto = new RegionDto
        {
            Id = region.Id,
            Code = region.Code,
            Name = region.Name,
            RegionImageUrl = region.RegionImageURL
        };

        return Ok(regionDto);
    }

    // Delete a region
    // DELETE: https://localhost:portnumber/api/Regions/1
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        // Get the region from the database
        var region = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        // If the region is null, return a 404
        if (region == null) return NotFound();

        // Remove the region from the database
        _dbContext.Regions.Remove(region);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}