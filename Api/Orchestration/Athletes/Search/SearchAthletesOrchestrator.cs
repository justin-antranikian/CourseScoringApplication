using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.Search;

public class SearchAthletesOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<AthleteSearchResultDto>> Get(int? locationId, string? locationType, string? searchTerm)
    {
        var query = dbContext.GetAthletesWithLocationInfo();

        if (searchTerm != null)
        {
            query = query.Where(oo => oo.FullName.Contains(searchTerm));
        }

        if (locationId.HasValue && locationType != null)
        {
            var type = Enum.Parse<LocationType>(locationType);
            var locationIdValue = locationId.Value;

            IQueryable<Athlete> GetQuery()
            {
                return type switch
                {
                    LocationType.State => query.Where(oo => oo.StateLocationId == locationIdValue),
                    LocationType.Area => query.Where(oo => oo.AreaLocationId == locationIdValue),
                    LocationType.City => query.Where(oo => oo.CityLocationId == locationIdValue),
                };
            }

            query = GetQuery();
        }

        var results = await query.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
        return results.Select(MapToDto).ToList();
    }

    private static AthleteSearchResultDto MapToDto(Athlete athlete)
    {
        return new AthleteSearchResultDto
        {
            Id = athlete.Id,
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.GetGenderFormatted(),
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
        };
    }
}
