using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
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
    }
}