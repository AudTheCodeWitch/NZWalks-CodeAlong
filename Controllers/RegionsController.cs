using AutoMapper;
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
    private readonly IMapper _mapper;

    public RegionsController(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    // GET all regions
    // GET: https://localhost:portnumber/api/Regions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get all regions from the database - domain models
        var regions = await _regionRepository.GetAllAsync();

        var regionsDto = _mapper.Map<List<RegionDto>>(regions);

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

        return Ok(_mapper.Map<RegionDto>(region));
    }

    // Create a new region
    // POST: https://localhost:portnumber/api/Regions
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto request)
    {
        // Map or convert the DTO to a domain model
        var regionDomainModel = _mapper.Map<Region>(request);

        // Add the region to the database
        regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

        // Map the domain model back to a DTO
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    // Update a region
    // PUT: https://localhost:portnumber/api/Regions/1
    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto request)
    {
        // Map DTO to domain model
        var regionDomainModel = _mapper.Map<Region>(request);
        
        // Get the region from the database
        var region = await _regionRepository.UpdateAsync(id, regionDomainModel);
        
        if (region == null) return NotFound();
        
        // Map the domain model back to a DTO
        var regionDto = _mapper.Map<RegionDto>(region);

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

        return Ok(_mapper.Map<RegionDto>(region));
    }
}