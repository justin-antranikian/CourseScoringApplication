using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Locations.GetDirectory;

public class GetDirectoryOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<DirectoryDto>> Get(int? locationId)
    {
        var locations = await dbContext.Locations.Include(oo => oo.ChildLocations).ThenInclude(oo => oo.ChildLocations).Where(oo => oo.LocationType == LocationType.State).ToListAsync();
        return locations.Select(oo => MapToDto(oo, locationId)).ToList();
    }

    private static DirectoryDto MapToDto(Location location, int? locationId)
    {
        var children = location.ChildLocations.Select(oo => MapToDto(oo, locationId)).ToList();
        var isSelected = location.Id == locationId;

        return new DirectoryDto
        {
            Id = location.Id,
            IsSelected = isSelected,
            IsExpanded = isSelected || children.Any(oo => oo.IsExpanded),
            LocationType = location.LocationType.ToString(),
            Name = location.Name,
            Slug = location.Slug,
            ChildLocations = children
        };
    }
}