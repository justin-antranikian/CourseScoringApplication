using DataModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace Orchestration.GetRaceSeriesSearch
{
	public class SearchEventsOrchestrator
	{
		private readonly ScoringDbContext _scoringDbContext;

		public SearchEventsOrchestrator(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		public async Task<List<EventSearchResultDto>> GetSearchResults(SearchEventsRequestDto raceSeriesRequest)
		{
			var baseQuery = _scoringDbContext.RaceSeries.AsQueryable();

			if (raceSeriesRequest.SearchTerm is string searchTerm)
			{
				baseQuery = baseQuery.Where(oo => oo.Name.StartsWith(searchTerm));
			}

			if (raceSeriesRequest.IsUpcoming is bool isUpcoming)
			{
				baseQuery = baseQuery.Where(oo => oo.IsUpcoming == isUpcoming);
			}

			if (raceSeriesRequest.RaceSeriesTypes.Any())
			{
				baseQuery = baseQuery.Where(oo => raceSeriesRequest.RaceSeriesTypes.Contains(oo.RaceSeriesType));
			}

			if (raceSeriesRequest.State is string stateUrl)
			{
				var location = LocationHelper.Find(oo => oo.StateUrlFriendly == stateUrl);
				baseQuery = baseQuery.Where(oo => oo.State == location.State);
			}

			if (raceSeriesRequest.Area is string areaUrl)
			{
				var location = LocationHelper.Find(oo => oo.AreaUrlFriendly == areaUrl);
				baseQuery = baseQuery.Where(oo => oo.Area == location.Area);
			}

			if (raceSeriesRequest.City is string cityUrl)
			{
				var location = LocationHelper.Find(oo => oo.CityUrlFriendly == cityUrl);
				baseQuery = baseQuery.Where(oo => oo.City == location.City);
			}

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
}
