using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.Reports.AthleteResultsReport;

public class AthleteResultsReportOrchestrator
{
	private readonly ScoringDbContext _scoringDbContext;

	public AthleteResultsReportOrchestrator(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	private static AthleteReportCourseDto GetAthleteReportCourseDto(List<Race> races, AthleteCourse athleteCourse)
	{
		var race = races.Single(oo => oo.Id == athleteCourse.Course.RaceId);
		var course = race.Courses.Single(oo => oo.Id == athleteCourse.CourseId);
		return new AthleteReportCourseDto
		(
			athleteCourse.CourseId,
			course.Name,
			race.RaceSeries.Name,
			race.KickOffDate,
			athleteCourse.Course.Distance,
			race.RaceSeries.RaceSeriesType
		);
	}

	private static AthleteReportDto GetAthleteReportDto(IGrouping<Athlete, AthleteCourse> athleteCourseGrouping, List<Race> races)
	{
		var courseResults = athleteCourseGrouping.Select(oo => GetAthleteReportCourseDto(races, oo)).ToList();
		var athlete = athleteCourseGrouping.Key;
		return new AthleteReportDto(athlete.Id, athlete.FirstName, athlete.LastName, athlete.FullName, courseResults);
	}

	private async Task<List<Race>> GetRaces(DateTime startOfYear, DateTime endOfYear)
	{
		var query = _scoringDbContext.Races
						.Include(oo => oo.Courses)
						.Include(oo => oo.RaceSeries)
						.Where(oo => oo.KickOffDate >= startOfYear && oo.KickOffDate <= endOfYear);

		return await query.ToListAsync();
	}

	private async Task<List<AthleteCourse>> GetAthleteCourses(IEnumerable<int> courseIds)
	{
		var query = _scoringDbContext.AthleteCourses
						.Include(oo => oo.Athlete)
						.Where(oo => courseIds.Contains(oo.CourseId));

		return await query.ToListAsync();
	}

	public async Task<List<AthleteReportDto>> GetAthleteReportDtos(DateTime dateTimeUtc)
	{
		var yearAsInt = dateTimeUtc.Year;

		var startOfYear = new DateTime(yearAsInt, 1, 1);
		var endOfYear = new DateTime(yearAsInt + 1, 1, 1);

		var races = await GetRaces(startOfYear, endOfYear);

		var courseIds = races.SelectMany(oo => oo.Courses).Select(oo => oo.Id);
		var athleteCourses = await GetAthleteCourses(courseIds);

		return athleteCourses.GroupBy(oo => oo.Athlete).Select(grouping => GetAthleteReportDto(grouping, races)).ToList();
	}
}
