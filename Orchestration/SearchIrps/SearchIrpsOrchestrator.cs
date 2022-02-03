using DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestration.SearchIrps;

public class SearchIrpsOrchestrator
{
	private readonly ScoringDbContext _scoringDbContext;

	public SearchIrpsOrchestrator(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	public async Task<List<IrpSearchResultDto>> GetSearchResults(SearchIrpsRequestDto searchRequestDto)
	{
		var baseQuery = GetBaseQuery(searchRequestDto);
		var filterQuery = GetFilterQuery(baseQuery, searchRequestDto.SearchOn, searchRequestDto.SearchTerm);
		var athleteCourses = await filterQuery.OrderBy(oo => oo.Athlete.LastName).ToListAsync();
		return IrpSearchResultDtoMapper.GetIrpSearchResultDto(athleteCourses);
	}

	private IQueryable<AthleteCourse> GetBaseQuery(SearchIrpsRequestDto searchRequestDto)
	{
		var baseQuery = _scoringDbContext.AthleteCourses.Include(oo => oo.Athlete).Include(oo => oo.Course).AsQueryable();

		if (searchRequestDto.RaceId is int raceId)
		{
			return baseQuery.Where(oo => oo.Course.RaceId == raceId);
		}

		if (searchRequestDto.CourseId is int courseId)
		{
			return baseQuery.Where(oo => oo.CourseId == courseId);
		}

		var errorMessage = $"'{nameof(searchRequestDto.RaceId)}' or '{nameof(searchRequestDto.CourseId)}' must be set.";
		throw new NotImplementedException(errorMessage);
	}

	private IQueryable<AthleteCourse> GetFilterQuery(IQueryable<AthleteCourse> baseQuery, SearchOnField searchOn, string searchTerm)
	{
		return searchOn switch
		{
			SearchOnField.Bib => baseQuery.Where(oo => oo.Bib.StartsWith(searchTerm)),
			SearchOnField.FirstName => baseQuery.Where(oo => oo.Athlete.FirstName.StartsWith(searchTerm)),
			SearchOnField.LastName => baseQuery.Where(oo => oo.Athlete.LastName.StartsWith(searchTerm)),
			SearchOnField.FullName => baseQuery.Where(oo => oo.Athlete.FullName.StartsWith(searchTerm)),
			_ => baseQuery
		};
	}
}
