using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.Search;

public class SearchRacesOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<EventSearchResultDto>> Get(SearchEventsRequestDto raceSeriesRequest)
    {
        var baseQuery = scoringDbContext.RaceSeries.AsQueryable();

        if (raceSeriesRequest.SearchTerm is string searchTerm)
        {
            baseQuery = baseQuery.Where(oo => oo.Name.StartsWith(searchTerm));
        }

        if (raceSeriesRequest.RaceSeriesTypes.Any())
        {
            baseQuery = baseQuery.Where(oo => raceSeriesRequest.RaceSeriesTypes.Contains(oo.RaceSeriesType));
        }

        //if (raceSeriesRequest.State is string stateUrl)
        //{
        //    var location = LocationHelper.Find(oo => oo.StateUrlFriendly == stateUrl);
        //    baseQuery = baseQuery.Where(oo => oo.State == location.State);
        //}

        //if (raceSeriesRequest.Area is string areaUrl)
        //{
        //    var location = LocationHelper.Find(oo => oo.AreaUrlFriendly == areaUrl);
        //    baseQuery = baseQuery.Where(oo => oo.Area == location.Area);
        //}

        //if (raceSeriesRequest.City is string cityUrl)
        //{
        //    var location = LocationHelper.Find(oo => oo.CityUrlFriendly == cityUrl);
        //    baseQuery = baseQuery.Where(oo => oo.City == location.City);
        //}

        var raceSeries = await baseQuery
                            .Include(oo => oo.Races)
                            .ThenInclude(oo => oo.Courses)
                            .OrderBy(oo => oo.OverallRank)
                            .Take(28)
                            .ToListAsync();

        var searchDtos = EventSearchResultDtoMapper.GetEventSearchResultDto(raceSeries);
        return searchDtos;
    }
}
