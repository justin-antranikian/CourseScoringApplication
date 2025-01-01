using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.Search;

public class SearchAthletesOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<AthleteSearchResultDto>> Get(int? locationId, string? locationType, string? searchTerm)
    {
        var baseQuery = dbContext.Athletes
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .Include(oo => oo.AthleteRaceSeriesGoals)
            .AsQueryable();

        if (searchTerm != null)
        {
            baseQuery = baseQuery.Where(oo => oo.FullName.Contains(searchTerm));
        }

        if (locationId.HasValue && locationType != null)
        {
            var type = Enum.Parse<LocationType>(locationType);
            var locationIdValue = locationId.Value;

            IQueryable<Athlete> GetQuery()
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
        return results.Select(AthleteSearchResultDtoMapper.GetAthleteSearchResultDto).ToList();
    }
}
