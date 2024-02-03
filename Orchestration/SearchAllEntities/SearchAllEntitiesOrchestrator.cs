using Microsoft.EntityFrameworkCore;
using Orchestration.AthletesSearch;
using Orchestration.GetRaceSeriesSearch;
using System.Threading.Tasks;

namespace Orchestration.GetSearchAllEntitiesSearch;

public class SearchAllEntitiesOrchestrator
{
    private readonly ScoringDbContext _scoringDbContext;

    public SearchAllEntitiesOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task<AllEntitiesSearchResultDto> GetSearchResults(string searchTerm)
    {
        var athletes = await GetAthletes(searchTerm);
        var raceSeries = await GetRaceSeries(searchTerm);
        return new AllEntitiesSearchResultDto(athletes, raceSeries);
    }

    private async Task<List<AthleteSearchResultDto>> GetAthletes(string searhTerm)
    {
        var query = _scoringDbContext.Athletes
                        .Include(oo => oo.AthleteRaceSeriesGoals)
                        .Where(oo => oo.FullName.StartsWith(searhTerm))
                        .OrderBy(oo => oo.OverallRank);

        var results = await query.ToListAsync();
        return AthleteSearchResultDtoMapper.GetAthleteSearchResultDto(results);
    }

    private async Task<List<EventSearchResultDto>> GetRaceSeries(string searchTerm)
    {
        var query = _scoringDbContext.RaceSeries
                        .Include(oo => oo.Races)
                        .ThenInclude(oo => oo.Courses)
                        .Where(oo => oo.Name.StartsWith(searchTerm))
                        .OrderBy(oo => oo.OverallRank);

        var results = await query.ToListAsync();
        return EventSearchResultDtoMapper.GetEventSearchResultDto(results);
    }
}
