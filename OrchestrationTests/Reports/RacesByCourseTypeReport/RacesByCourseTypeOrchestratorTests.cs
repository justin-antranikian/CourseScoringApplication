using DataModels;
using Orchestration.Reports.RacesByCourseTypeReport;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.Reports.Report;

public class RacesByCourseTypeOrchestratorTests
{
	[Fact]
	public async Task RacesByCourseTypeOrchestrator_MapsAllFields()
	{
		var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

		var initialRace = new Race
		{
			Id = 1,
			Courses = new ()
			{
				new () { Name =  "A", CourseType = Core.CourseType.Running5K, Distance = 5000 }
			},
			KickOffDate = new DateTime(2020, 5, 1),
			RaceSeries = new RaceSeries
			{
				Name = "B",
			}
		};

		var races = new List<Race>
		{
			initialRace,
		};

		await dbContext.Races.AddRangeAsync(races);
		await dbContext.SaveChangesAsync();

		var orchestrator = new RacesByCourseTypeOrchestrator(dbContext);
		var dtos = await orchestrator.GetReport(new DateTime(2020, 5, 1));

		var (firstResultCourseName, firstResultRaces) = dtos[0];

		Assert.Equal("5 K", firstResultCourseName);

		var (raceSeriesName, kickOffDate, distance) = firstResultRaces[0];

		Assert.Equal("B", raceSeriesName);
		Assert.Equal(new DateTime(2020, 5, 1), kickOffDate);
		Assert.Equal(5000, distance);
	}
}
