using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.Reports.RacesByMonthReport;

public class RacesByMonthReportOrchestrator
{
	private readonly ScoringDbContext _scoringDbContext;

	public RacesByMonthReportOrchestrator(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	private static ReportByMonthRaceDto MapReportByMonthRaceDto(Race race)
	{
		return new ReportByMonthRaceDto(race.Id, race.RaceSeries.Name, race.KickOffDate, race.RaceSeries.RaceSeriesType);
	}

	public async Task<List<ReportByMonthDto>> GetReportByMonthDtos(DateTime dateTimeUtc)
	{
		var yearAsInt = dateTimeUtc.Year;

		var startOfYear = new DateTime(yearAsInt, 1, 1);
		var endOfYear = new DateTime(yearAsInt + 1, 1, 1);

		var races = await _scoringDbContext.Races.Include(oo => oo.RaceSeries).Where(oo => oo.KickOffDate >= startOfYear && oo.KickOffDate <= endOfYear).ToListAsync();

		var reportByMonthDtos = races.GroupBy(oo => oo.KickOffDate.Month).OrderBy(oo => oo.Key).Select(grouping =>
		{
			var monthAsString = grouping.First().KickOffDate.ToString("MMMM");
			var raceDtos = grouping.OrderBy(oo => oo.KickOffDate).Select(MapReportByMonthRaceDto).ToList();
			return new ReportByMonthDto(monthAsString, raceDtos);
		});

		return reportByMonthDtos.ToList();
	}
}

