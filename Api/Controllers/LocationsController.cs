using Api.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public record LocationDto
{
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public List<LocationDto> ChildLocations { get; init; } = [];
}

[ApiController]
[Route("locations")]
public class LocationsController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<List<LocationDto>> Get()
    {
        var locations = await scoringDbContext.Locations.Include(oo => oo.ChildLocations).ThenInclude(oo => oo.ChildLocations).Where(oo => oo.LocationType == LocationType.State).ToListAsync();

        var locationDtos = new List<LocationDto>();
        foreach (var location in locations)
        {
            var stateLocation = new LocationDto
            {
                Name = location.Name,
                Slug = location.Slug
            };

            foreach (var areaLocation in location.ChildLocations)
            {
                var areaLocationDto = new LocationDto
                {
                    Name = areaLocation.Name,
                    Slug = areaLocation.Slug
                };

                foreach (var cityLocation in areaLocation.ChildLocations)
                {
                    areaLocationDto.ChildLocations.Add(new LocationDto
                    {
                        Name = cityLocation.Name,
                        Slug = cityLocation.Slug
                    });
                }

                stateLocation.ChildLocations.Add(areaLocationDto);
            }

            locationDtos.Add(stateLocation);
        }

        return locationDtos;
    }

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