using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.Search;

public class SearchAthletesOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<AthleteSearchResultDto>> GetSearchResults(SearchAthletesRequestDto searchRequestDto)
    {
        var baseQuery = scoringDbContext.Athletes
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .Include(oo => oo.AthleteRaceSeriesGoals)
            .AsQueryable();

        var athletes = await baseQuery.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
        return athletes.Select(AthleteSearchResultDtoMapper.GetAthleteSearchResultDto).ToList();
    }
}
