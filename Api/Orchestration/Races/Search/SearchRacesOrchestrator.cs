using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.Search;

public class SearchRacesOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<EventSearchResultDto>> Get(SearchEventsRequestDto raceSeriesRequest)
    {
        var baseQuery = scoringDbContext.RaceSeries.AsQueryable();

        var raceSeries = await baseQuery
                            .Include(oo => oo.StateLocation)
                            .Include(oo => oo.AreaLocation)
                            .Include(oo => oo.CityLocation)
                            .Include(oo => oo.Races)
                            .ThenInclude(oo => oo.Courses)
                            .OrderBy(oo => oo.OverallRank)
                            .Take(28)
                            .ToListAsync();

        return EventSearchResultDtoMapper.GetEventSearchResultDto(raceSeries);
    }
}
