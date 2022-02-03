using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Orchestration.GetCompetetorsForIrp;

public class GetCompetetorsForIrpOrchestrator
{
	private readonly ScoringDbContext _scoringDbContext;

	public GetCompetetorsForIrpOrchestrator(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	public async Task<GetCompetetorsForIrpDto> GetCompetetorsForIrpResult(int athleteCourseId)
	{
		var primaryBracket = await _scoringDbContext.AtheleteCourseBrackets.SingleAsync(oo => oo.AthleteCourseId == athleteCourseId && oo.Bracket.BracketType == BracketType.Overall);
		var result = await _scoringDbContext.Results.Include(oo => oo.AthleteCourse).SingleAsync(oo => oo.IsHighestIntervalCompleted && oo.AthleteCourseId == athleteCourseId && oo.BracketId == primaryBracket.BracketId);

		var course = await _scoringDbContext.Courses.SingleAsync(oo => oo.Id == result.AthleteCourse.CourseId);
		var interval = await _scoringDbContext.Intervals.SingleAsync(oo => oo.Id == result.IntervalId);

		var results = await GetResultsNearAthleteCourse(result, primaryBracket.BracketId);

		var fasterResults = results.Where(oo => oo.Rank < result.Rank).OrderByDescending(oo => oo.Rank);
		var slowerResults = results.Where(oo => oo.Rank > result.Rank).OrderBy(oo => oo.Rank);

		IrpCompetetorDto MapToIrpCompetetorDto(Result result)
		{
			return IrpCompetetorDtoMapper.GetIrpCompetetorDto(result, course, interval);
		}

		var fasterAthletes = fasterResults.Select(MapToIrpCompetetorDto).ToList();
		var slowerAthletes = slowerResults.Select(MapToIrpCompetetorDto).ToList();

		return new GetCompetetorsForIrpDto(fasterAthletes, slowerAthletes);
	}

	private async Task<List<Result>> GetResultsNearAthleteCourse(Result result, int primaryBracketId)
	{
		var highRank = result.Rank + 3;
		var lowRank = result.Rank - 3;

		var query = _scoringDbContext.Results
						.Include(oo => oo.AthleteCourse)
						.ThenInclude(oo => oo.Athlete)
						.Where(oo =>
							oo.IntervalId == result.IntervalId &&
							oo.BracketId == primaryBracketId &&
							oo.IsHighestIntervalCompleted == true &&
							oo.Rank >= lowRank &&
							oo.Rank <= highRank
						);

		return await query.ToListAsync();
	}
}
