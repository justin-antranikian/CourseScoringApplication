using Api.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Locations.GetDetails;

public class GetLocationDetailOrchestrator(ScoringDbContext dbContext)
{
    public async Task<IActionResult> Get(string slug)
    {
        var location = await dbContext.Locations.SingleOrDefaultAsync(oo => oo.Slug == slug.ToLower());

        if (location == null)
        {
            return new NotFoundObjectResult($"Could not find location by slug. Slug: ({slug}).");
        }

        return new OkObjectResult(new LocationDto
        {
            Id = location.Id,
            LocationType = location.LocationType.ToString(),
            Name = location.Name,
            Slug = location.Slug,
        });
    }
}