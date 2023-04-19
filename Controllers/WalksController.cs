using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

// /api/walks
[Route("api/[controller]")]
[ApiController]
public class WalksController : Controller
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepository;

    public WalksController(IMapper mapper, IWalkRepository walkRepository)
    {
        _mapper = mapper;
        _walkRepository = walkRepository;
    }

    // Create
    // POST: api/walks
    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        // Map DTO to Domain Model
        var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

        await _walkRepository.CreateAsync(walkDomainModel);

        // Map Domain Model to DTO
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }

    // Read
    // GET: api/walks?filterOn=Name&filterQuery=Track&SortBy=Name&isAscending=true
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? filterOn,
        [FromQuery] string? filterQuery,
        [FromQuery] string? sortBy,
        [FromQuery] bool? isAscending = true)
    {
        var walksDomainModel = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true);

        // Map Domain Model to DTO
        var walksDto = _mapper.Map<List<WalkDto>>(walksDomainModel);
        return Ok(walksDto);
    }

    // GET: api/walks/{id}
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomainModel = await _walkRepository.GetByIdAsync(id);

        if (walkDomainModel == null) return NotFound();

        // Map Domain Model to DTO
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }

    // Update
    // PUT: api/walks/{id}
    [HttpPut]
    [Route("{id:guid}")]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
    {
        // Map DTO to Domain Model
        var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

        walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

        if (walkDomainModel == null) return NotFound();

        // Map Domain Model to DTO
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }

    // Delete
    // DELETE: api/walks/{id}
    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var walkDomainModel = await _walkRepository.DeleteAsync(id);

        if (walkDomainModel == null) return NotFound();

        // Map Domain Model to DTO
        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }
}