using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.CompareAthletes;

public class CompareAthletesOrchestrator
{
	private readonly ScoringDbContext _scoringDbContext;

	public CompareAthletesOrchestrator(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	public async Task<List<CompareAthletesAthleteInfoDto>> GetCompareAthletesDto(int[] athleteIds)
	{
		athleteIds = athleteIds.Take(4).ToArray();
		var athletes = await GetAthletes(athleteIds);
		var results = await GetResults(athleteIds);

		var distinctCourseIds = results.Select(oo => oo.CourseId).Distinct().ToArray();
		var courses = await GetCourses(distinctCourseIds);

		var helper = new CompareAthletesHelper(courses, athletes, results);
		return helper.GetMappedResults();
	}

	private async Task<List<Athlete>> GetAthletes(int[] athleteIds)
	{
		var query = _scoringDbContext.Athletes
						.Where(oo => athleteIds.Contains(oo.Id))
						.OrderBy(oo => oo.FullName);

		return await query.ToListAsync();
	}

	private async Task<List<Result>> GetResults(int[] athleteIds)
	{
		var query = _scoringDbContext.Results
						.Include(oo => oo.AthleteCourse)
						.Where(oo => athleteIds.Contains(oo.AthleteCourse.AthleteId))
						.Where(oo => oo.Bracket.BracketType == BracketType.Overall && oo.IsHighestIntervalCompleted);

		return await query.ToListAsync();
	}

	private async Task<List<Course>> GetCourses(int[] courseIds)
	{
		var query = _scoringDbContext.Courses
						.Include(oo => oo.Race)
						.ThenInclude(oo => oo.RaceSeries)
						.Where(oo => courseIds.Contains(oo.Id));

		return await query.ToListAsync();
	}
}
