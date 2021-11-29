using DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestration.Reports.RacesByMonthReport
{
	public class ReportOrchestrator
	{
		private readonly ScoringDbContext _scoringDbContext;

		public ReportOrchestrator(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		public async Task<List<ReportByMonthDto>> GetReport(DateTime dateTimeUtc)
		{
			var yearAsInt = dateTimeUtc.Year;

			var startOfYear = new DateTime(yearAsInt, 1, 1);
			var endOfYear = new DateTime(yearAsInt + 1, 1, 1);

			var allRaces = await _scoringDbContext.Races.Include(oo => oo.Courses).Include(oo => oo.RaceSeries).ToListAsync();

			foreach (var grouping in allRaces.GroupBy(oo => oo.RaceSeries.RaceSeriesType))
			{
				//var x = grouping.Group

			}

			return null;
		}
	}
}
