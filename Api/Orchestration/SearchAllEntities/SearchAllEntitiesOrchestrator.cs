using Api.DataModels;
using Api.Orchestration.Athletes.Search;
using Api.Orchestration.Races.Search;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.SearchAllEntities;

public class SearchAllEntitiesOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<AllEntitiesSearchResultDto> GetSearchResults(string searchTerm)
    {
        var athletes = await GetAthletes(searchTerm);
        var raceSeries = await GetRaceSeries(searchTerm);
        return new AllEntitiesSearchResultDto(athletes, raceSeries);
    }

    private async Task<List<AthleteSearchResultDto>> GetAthletes(string searhTerm)
    {
        var query = scoringDbContext.Athletes
                        .Include(oo => oo.AthleteRaceSeriesGoals)
                        .Include(oo => oo.AreaLocation)
                        .Include(oo => oo.CityLocation)
                        .Include(oo => oo.StateLocation)
                        .Where(oo => oo.FullName.StartsWith(searhTerm))
                        .OrderBy(oo => oo.OverallRank);

        var results = await query.ToListAsync();
        return AthleteSearchResultDtoMapper.GetAthleteSearchResultDto(results);
    }

    private async Task<List<EventSearchResultDto>> GetRaceSeries(string searchTerm)
    {
        var query = scoringDbContext.RaceSeries
                        .Include(oo => oo.Races)
                        .ThenInclude(oo => oo.Courses)
                        .Where(oo => oo.Name.StartsWith(searchTerm))
                        .OrderBy(oo => oo.OverallRank);

        var results = await query.ToListAsync();
        return EventSearchResultDtoMapper.GetEventSearchResultDto(results);
    }
}
