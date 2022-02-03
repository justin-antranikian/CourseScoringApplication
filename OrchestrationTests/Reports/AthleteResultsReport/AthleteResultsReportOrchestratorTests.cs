using DataModels;
using Orchestration.Reports.AthleteResultsReport;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.Reports.RacesByMonthReport;

public class AthleteResultsReportOrchestratorTests
{
	[Fact]
	public async Task AthleteResultsReportOrchestrator_MapsAllFields()
	{
		var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

		var firstCourse = new Course
		{
			Id = 10,
			Name = "Course 1",
			RaceId = 1,
			Distance = 1000
		};

		var primaryRace = new Race
		{
			Id = 1,
			KickOffDate = new DateTime(2020, 5, 5),
			RaceSeries = new ()
			{
				RaceSeriesType = Core.RaceSeriesType.RoadBiking,
				Name = "Race Series 1"
			},
			Courses = new ()
			{
				firstCourse
			}
		};

		var secondCourse = new Course { Id = 20, RaceId = 2 };

		var secondRace = new Race
		{
			Id = 2,
			KickOffDate = new DateTime(2021, 5, 5),
			Courses = new ()
			{
				secondCourse
			}
		};

		var thirdCourse = new Course { Id = 30, RaceId = 3 };

		var thirdRace = new Race
		{
			Id = 3,
			KickOffDate = new DateTime(2019, 5, 5),
			Courses = new ()
			{
				thirdCourse
			}
		};

		var races = new List<Race>
		{
			primaryRace,
			secondRace,
			thirdRace
		};

		var firstAthlete = new Athlete
		{
			Id = 1,
			FirstName = "jason",
			LastName = "last",
			FullName = "jason last",
			City = "ca"
		};

		var athleteCourses = new List<AthleteCourse>
		{
			new () { Id = 1, Athlete = firstAthlete, CourseId = 10, Course = firstCourse },
			new () { Id = 2, Athlete = firstAthlete, CourseId = 20, Course = secondCourse },
			new () { Id = 3, Athlete = firstAthlete, CourseId = 30, Course = thirdCourse },
		};

		await dbContext.Races.AddRangeAsync(races);
		await dbContext.AthleteCourses.AddRangeAsync(athleteCourses);
		await dbContext.SaveChangesAsync();

		var orchestrator = new AthleteResultsReportOrchestrator(dbContext);
		var athleteReportDtos = await orchestrator.GetAthleteReportDtos(new DateTime(2020, 5, 1));

		Assert.Collection(athleteReportDtos, athleteReportDto =>
		{
			Assert.Equal(1, athleteReportDto.AthleteId);
			Assert.Equal("jason", athleteReportDto.FirstName);
			Assert.Equal("last", athleteReportDto.LastName);
			Assert.Equal("jason last", athleteReportDto.FullName);
			Assert.Collection(athleteReportDto.AthleteReportCourseDtos, course =>
			{
				Assert.Equal(10, course.CourseId);
				Assert.Equal("Course 1", course.CourseName);
				Assert.Equal("Race Series 1", course.RaceSeriesName);
				Assert.Equal(new DateTime(2020, 5, 5), course.KickOffDate);
				Assert.Equal(1000, course.CourseDistance);
				Assert.Equal(Core.RaceSeriesType.RoadBiking, course.RaceSeriesType);
			});
		});
	}
}
