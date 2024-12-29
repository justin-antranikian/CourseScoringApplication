using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.Search;

public class SearchRacesOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<EventSearchResultDto>> Get(int? locationId, string? locationType, string? searchTerm)
    {
        var baseQuery = scoringDbContext.RaceSeries
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .Include(oo => oo.Races)
            .ThenInclude(oo => oo.Courses)
            .AsQueryable();

        if (searchTerm != null)
        {
            baseQuery = baseQuery.Where(oo => oo.Name.Contains(searchTerm));
        }

        if (locationId.HasValue && locationType != null)
        {
            var type = Enum.Parse<LocationType>(locationType);
            var locationIdValue = locationId.Value;

            IQueryable<RaceSeries> GetQuery()
            {
                return type switch
                {
                    LocationType.State => baseQuery.Where(oo => oo.StateLocationId == locationIdValue),
                    LocationType.Area => baseQuery.Where(oo => oo.AreaLocationId == locationIdValue),
                    LocationType.City => baseQuery.Where(oo => oo.CityLocationId == locationIdValue),
                };
            }

            baseQuery = GetQuery();
        }

        var results = await baseQuery.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
        return results.Select(EventSearchResultDtoMapper.GetEventSearchResultDto).ToList();
    }
}
