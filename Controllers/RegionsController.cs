using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")] // api/Regions
[ApiController]
public class RegionsController : Controller
{
    private readonly NZWalksDbContext dbContext;

    public RegionsController(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    // GET all regions
    // GET: https://localhost:5001/api/Regions
    [HttpGet]
    public IActionResult GetAll()
    {
        // Get all regions from the database
        var regions = dbContext.Regions.ToList();

        return Ok(regions);
    }

    // GET a region by id
    // GET: https://localhost:5001/api/Regions/1
    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        // Get a region from the database
        var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

        // If the region is null, return a 404
        if (region == null) return NotFound();

        return Ok(region);
    }
}