using Api.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public record LocationDto
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
}

[ApiController]
[Route("locations")]
public class LocationsController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet("by-slug")]
    public async Task<IActionResult> Get([FromQuery] string slug)
    {
        var location = await scoringDbContext.Locations.SingleOrDefaultAsync(oo => oo.Slug == slug.ToLower());

        if (location == null)
        {
            return NotFound($"Could not find location by slug. Slug: ({slug}).");
        }

        return Ok(new LocationDto
        {
            Name = location.Name,
            Slug = location.Slug
        });
    }
}