using Core;
using DataModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestration.AthletesSearch
{
	public class SearchAthletesOrchestrator
	{
		private readonly ScoringDbContext _scoringDbContext;

		public SearchAthletesOrchestrator(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		public async Task<List<AthleteSearchResultDto>> GetSearchResults(SearchAthletesRequestDto searchRequestDto)
		{
			var baseQuery = _scoringDbContext.Athletes.Include(oo => oo.AthleteRaceSeriesGoals).AsQueryable();

			if (searchRequestDto.SearchTerm is string searchTerm)
			{
				baseQuery = baseQuery.Where(oo => oo.FullName.StartsWith(searchTerm));
			}

			if (searchRequestDto.State is string stateUrl)
			{
				var location = LocationHelper.Find(oo => oo.StateUrlFriendly == stateUrl);
				baseQuery = baseQuery.Where(oo => oo.State == location.State);
			}

			if (searchRequestDto.Area is string areaUrl)
			{
				var location = LocationHelper.Find(oo => oo.AreaUrlFriendly == areaUrl);
				baseQuery = baseQuery.Where(oo => oo.Area == location.Area);
			}

			if (searchRequestDto.City is string cityUrl)
			{
				var location = LocationHelper.Find(oo => oo.CityUrlFriendly == cityUrl);
				baseQuery = baseQuery.Where(oo => oo.City == location.City);
			}

			var athletes = await baseQuery.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
			return athletes.Select(oo => AthleteSearchResultDtoMapper.GetAthleteSearchResultDto(oo)).ToList();
		}
	}
}
