using Api.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public record LocationDto
{
    public required int Id { get; init; }
    public bool IsSelected { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public List<LocationDto> ChildLocations { get; init; } = [];
    public bool IsExpanded { get; set; }
}

[ApiController]
[Route("locations")]
public class LocationsController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet("directory")]
    public async Task<List<LocationDto>> Get([FromQuery] int? locationId)
    {
        var locations = await scoringDbContext.Locations.Include(oo => oo.ChildLocations).ThenInclude(oo => oo.ChildLocations).Where(oo => oo.LocationType == LocationType.State).ToListAsync();

        var locationDtos = new List<LocationDto>();
        foreach (var location in locations)
        {
            var stateLocation = new LocationDto
            {
                Name = location.Name,
                Slug = location.Slug,
                Id = location.Id,
                IsSelected = location.Id == locationId
            };

            foreach (var areaLocation in location.ChildLocations)
            {
                var areaLocationDto = new LocationDto
                {
                    Name = areaLocation.Name,
                    Slug = areaLocation.Slug,
                    Id = areaLocation.Id,
                    IsSelected = areaLocation.Id == locationId
                };

                foreach (var cityLocation in areaLocation.ChildLocations)
                {
                    areaLocationDto.ChildLocations.Add(new LocationDto
                    {
                        Name = cityLocation.Name,
                        Slug = cityLocation.Slug,
                        Id = cityLocation.Id,
                        IsSelected = cityLocation.Id == locationId,
                        IsExpanded = cityLocation.Id == locationId
                    });
                }

                areaLocationDto.IsExpanded = areaLocationDto.ChildLocations.Any(oo => oo.IsExpanded) || areaLocationDto.IsSelected;
                stateLocation.ChildLocations.Add(areaLocationDto);
            }

            stateLocation.IsExpanded = stateLocation.ChildLocations.Any(oo => oo.IsExpanded) || stateLocation.IsSelected;
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
            Slug = location.Slug,
            Id = location.Id
        });
    }
}