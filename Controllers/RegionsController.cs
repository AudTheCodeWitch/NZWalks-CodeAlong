using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")] // api/Regions
[ApiController]
public class RegionsController : Controller
{
    private readonly IRegionRepository _regionRepository;

    public RegionsController(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    // GET all regions
    // GET: https://localhost:portnumber/api/Regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get all regions from the database - domain models
        var regions = await _regionRepository.GetAllAsync();

        // Map domain models to DTOs
        var regionsDto = regions.Select(region =>
        {
            if (region != null)
                return new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageURL
                };
            throw new InvalidOperationException();
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
        var region = await _regionRepository.GetByIdAsync(id);

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
        regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

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
        // Map DTO to domain model
        var regionDomainModel = new Region
        {
            Code = request.Code,
            Name = request.Name,
            RegionImageURL = request.RegionImageUrl
        };
        
        // Get the region from the database
        var region = await _regionRepository.UpdateAsync(id, regionDomainModel);
        
        if (region == null) return NotFound();
        
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
        var region = await _regionRepository.DeleteAsync(id);
        
        if (region == null) return NotFound();
        
        return NoContent();
    }
}