using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")] // api/Regions
    [ApiController]
    
    public class RegionsController : Controller
    {
        // GET all regions
        // GET: https://localhost:5001/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auukland",
                    Code = "AKL",
                    RegionImageURL = 
                        "https://www.aucklandnz.com/assets/Uploads/hero-images/hero-image-auckland-city.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageURL =
                        "https://www.wellingtonnz.com/assets/Uploads/hero-images/hero-image-wellington-city.jpg"
                },
            };
            return Ok(regions);
        }
    }
}