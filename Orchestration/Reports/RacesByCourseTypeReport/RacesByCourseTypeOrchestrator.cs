using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.Reports.RacesByCourseTypeReport;

public record ReportByCourseTypeGroupingDto(string CourseTypeName, List<ReportByCourseTypeRaceEntryDto> Races);

public record ReportByCourseTypeRaceEntryDto(string RaceSeriesName, DateTime KickOffDate, double Distance);

public class RacesByCourseTypeOrchestrator
{
	private readonly ScoringDbContext _scoringDbContext;

	public RacesByCourseTypeOrchestrator(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	public async Task<List<ReportByCourseTypeGroupingDto>> GetReport(DateTime dateTimeUtc)
	{
		var yearAsInt = dateTimeUtc.Year;

		var startOfYear = new DateTime(yearAsInt, 1, 1);
		var endOfYear = new DateTime(yearAsInt + 1, 1, 1);

		var allRaces = await _scoringDbContext.Races.Include(oo => oo.Courses).Include(oo => oo.RaceSeries).ToListAsync();

		var groupingResults = allRaces.SelectMany(oo => oo.Courses).GroupBy(oo => oo.CourseType).Select(grouping =>
		{
			var orderedCourses = grouping.OrderBy(oo => oo.Race.KickOffDate);
			var raceEntries = orderedCourses.Select(oo => new ReportByCourseTypeRaceEntryDto(oo.Race.RaceSeries.Name, oo.Race.KickOffDate, oo.Distance)).ToList();
			return new ReportByCourseTypeGroupingDto(grouping.Key.ToFriendlyText(), raceEntries);
		});

		return groupingResults.ToList();
	}
}
