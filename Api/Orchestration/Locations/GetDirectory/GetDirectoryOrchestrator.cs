using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Locations.GetDirectory;

public class GetDirectoryOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<LocationDto>> Get(int? locationId)
    {
        var locations = await dbContext.Locations.Include(oo => oo.ChildLocations).ThenInclude(oo => oo.ChildLocations).Where(oo => oo.LocationType == LocationType.State).ToListAsync();

        var locationDtos = new List<LocationDto>();
        foreach (var location in locations)
        {
            var stateLocation = new LocationDto
            {
                LocationType = location.LocationType.ToString(),
                Name = location.Name,
                Slug = location.Slug,
                Id = location.Id,
                IsSelected = location.Id == locationId
            };

            foreach (var areaLocation in location.ChildLocations)
            {
                var areaLocationDto = new LocationDto
                {
                    LocationType = areaLocation.LocationType.ToString(),
                    Name = areaLocation.Name,
                    Slug = areaLocation.Slug,
                    Id = areaLocation.Id,
                    IsSelected = areaLocation.Id == locationId
                };

                foreach (var cityLocation in areaLocation.ChildLocations)
                {
                    areaLocationDto.ChildLocations.Add(new LocationDto
                    {
                        LocationType = cityLocation.LocationType.ToString(),
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
}