using DataModels;
using Orchestration.Reports.RacesByMonthReport;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.Reports.RacesByMonthReport
{
	public class RacesByMonthReportOrchestratorTests
	{
		[Fact]
		public async Task RacesByMonthReportOrchestrator_MapsAllFields()
		{
			var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

			var initialRace = new Race { Id = 1, KickOffDate = new DateTime(2019, 5, 1), RaceSeries = new RaceSeries() };

			var races = new List<Race>
			{
				initialRace,
				initialRace with { Id = 2, KickOffDate = new DateTime(2020, 5, 5) },
				initialRace with { Id = 3, KickOffDate = new DateTime(2020, 5, 2) },
				initialRace with { Id = 4, KickOffDate = new DateTime(2020, 4, 1) },
				initialRace with { Id = 5, KickOffDate = new DateTime(2021, 5, 1) },
			};

			await dbContext.Races.AddRangeAsync(races);
			await dbContext.SaveChangesAsync();

			var orchestrator = new RacesByMonthReportOrchestrator(dbContext);
			var dtos = await orchestrator.GetReportByMonthDtos(new DateTime(2020, 5, 1));

			Assert.Collection(dtos, dto =>
			{
				Assert.Equal("April", dto.MonthName);
				Assert.Single(dto.Races);
			},
			dto => {
				Assert.Equal("May", dto.MonthName);
				Assert.Collection(dto.Races, race =>
				{
					Assert.Equal(3, race.Id);
				}, race =>
				{
					Assert.Equal(2, race.Id);
				});
			});
		}
	}
}
