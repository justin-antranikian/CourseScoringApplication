using Core;
using DataModels;
using Orchestration.CompareAthletes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.CompareAthletes
{
	public class CompareAthletesOrchestratorTests
	{
		[Fact]
		public async Task GetCompareAthletesResult_ReturnsCorrectResults()
		{
			var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

			var athletes = new[]
			{
				new Athlete { Id = 1, FullName = "FA", State = "SA", Area = "AA", City = "CA",  OverallRank = 15, StateRank = 10, AreaRank = 8, CityRank = 5, DateOfBirth = new DateTime(2010, 1, 1), Gender = Gender.Femail },
				new Athlete { Id = 2, FullName = "FA2" }
			};

			var overallBracket = new Bracket { Id = 1, BracketType = BracketType.Overall };
			var genderBracket = new Bracket { Id = 2, BracketType = BracketType.Gender };

			var athleteCourses = new[]
			{
				new AthleteCourse { Id = 1, AthleteId = 1, CourseId = 2 },
				new AthleteCourse { Id = 400, AthleteId = 400, CourseId = 2 },
				new AthleteCourse { Id = 2, AthleteId = 1, CourseId = 3 },
				new AthleteCourse { Id = 3, AthleteId = 2, CourseId = 4 },
			};

			var courses = new[]
			{
				new Course
				{
					Id = 2,
					Name = "Test Course 2",
					Distance = 1000,
					CourseType = CourseType.Running10K,
					Race = new Race
					{
						Id = 2,
						Name = "Test Race 2",
						RaceSeries = new RaceSeries
						{
							Name = "Test Race Series 2",
							RaceSeriesType = RaceSeriesType.Running
						}
					}
				},
				new Course
				{
					Id = 3,
					Name = "Test Course 3",
					Distance = 1500,
					CourseType = CourseType.Running25K,
					Race = new Race
					{
						Id = 3,
						Name = "Test Race 3",
						RaceSeries = new RaceSeries
						{
							Name = "Test Race Series 3",
							RaceSeriesType = RaceSeriesType.Running
						}
					}
				},
				new Course
				{
					Id = 4,
					Name = "Test Course 4",
					Distance = 5000,
					CourseType = CourseType.DownhillBikeRace,
					Race = new Race
					{
						Id = 4,
						Name = "Test Race 4",
						RaceSeries = new RaceSeries
						{
							Name = "Test Race Series 4",
							RaceSeriesType = RaceSeriesType.MountainBiking
						}
					}
				}
			};

			var results = new[]
			{
				new Result // result is used.
				{
					BracketId = 1,
					Bracket = overallBracket,
					AthleteCourse = athleteCourses[0],
					AthleteCourseId = 1,
					IsHighestIntervalCompleted = true,
					OverallRank = 20,
					GenderRank = 10,
					DivisionRank = 5,
					CourseId = 2,
					TimeOnCourse = 1000
				},
				new Result // result is used.
				{
					BracketId = 1,
					Bracket = overallBracket,
					AthleteCourse = athleteCourses[2],
					AthleteCourseId = 1,
					IsHighestIntervalCompleted = true,
					OverallRank = 19,
					GenderRank = 9,
					DivisionRank = 4,
					CourseId = 3,
					TimeOnCourse = 1500
				},
				new Result
				{
					BracketId = 2,
					Bracket = genderBracket,
					AthleteCourse = athleteCourses[0],
					AthleteCourseId = 1,
					IsHighestIntervalCompleted = true,
					OverallRank = 20,
					GenderRank = 10,
					DivisionRank = 5,
					CourseId = 2
				},
				new Result
				{
					BracketId = 1,
					Bracket = overallBracket,
					AthleteCourse = athleteCourses[0],
					AthleteCourseId = 1,
					IsHighestIntervalCompleted = false,
					OverallRank = 15,
					CourseId = 2
				},
				new Result
				{
					BracketId = 1,
					Bracket = overallBracket,
					AthleteCourse = athleteCourses[1],
					AthleteCourseId = 400,
					IsHighestIntervalCompleted = true,
					OverallRank = 21,
					GenderRank = 11,
					DivisionRank = 6,
					CourseId = 2
				},
				new Result // result is used.
				{
					BracketId = 1,
					Bracket = overallBracket,
					AthleteCourse = athleteCourses[3],
					AthleteCourseId = 3,
					IsHighestIntervalCompleted = true,
					OverallRank = 21,
					GenderRank = 11,
					DivisionRank = 6,
					CourseId = 4,
					TimeOnCourse = 2000
				},
			};

			await dbContext.Athletes.AddRangeAsync(athletes);
			await dbContext.Results.AddRangeAsync(results);
			await dbContext.Courses.AddRangeAsync(courses);
			await dbContext.SaveChangesAsync();

			var compareIrpsOrchestrator = new CompareAthletesOrchestrator(dbContext);
			var idsToCompare = new[] { 1, 2 };
			var athleteInfoDtos = await compareIrpsOrchestrator.GetCompareAthletesDto(idsToCompare);

			Assert.Collection(athleteInfoDtos, athlete =>
			{
				Assert.Equal("FA", athlete.FullName);
				Assert.True(athlete.Age >= 11);
				Assert.Equal("F", athlete.GenderAbbreviated);
				Assert.Equal("SA", athlete.LocationInfoWithRank.State);
				Assert.Equal("AA", athlete.LocationInfoWithRank.Area);
				Assert.Equal("CA", athlete.LocationInfoWithRank.City);
				Assert.Equal(15, athlete.LocationInfoWithRank.OverallRank);
				Assert.Equal(10, athlete.LocationInfoWithRank.StateRank);
				Assert.Equal(8, athlete.LocationInfoWithRank.AreaRank);
				Assert.Equal(5, athlete.LocationInfoWithRank.CityRank);
				Assert.Collection(athlete.Results, result =>
				{
					Assert.Equal(1, result.AthleteCourseId);
					Assert.Equal(2, result.RaceId);
					Assert.Equal("Test Race 2", result.RaceName);
					Assert.Equal(2, result.CourseId);
					Assert.Equal("Test Course 2", result.CourseName);
					Assert.Equal(20, result.OverallRank);
					Assert.Equal(10, result.GenderRank);
					Assert.Equal(5, result.DivisionRank);
					Assert.Equal("16:40", result.PaceWithTime.TimeFormatted);
				}, result =>
				{
					Assert.Equal(2, result.AthleteCourseId);
					Assert.Equal(3, result.RaceId);
					Assert.Equal("Test Race 3", result.RaceName);
					Assert.Equal(3, result.CourseId);
					Assert.Equal("Test Course 3", result.CourseName);
					Assert.Equal(19, result.OverallRank);
					Assert.Equal(9, result.GenderRank);
					Assert.Equal(4, result.DivisionRank);
					Assert.Equal("25:00", result.PaceWithTime.TimeFormatted);
				});

				Assert.Collection(athlete.Stats, stat =>
				{
					Assert.Equal("Running", stat.RaceSeriesTypeName);
					Assert.Equal(2, stat.ActualTotal);
					Assert.Equal(2500, stat.TotalDistance);
				});
			}, athlete =>
			{
				Assert.Equal("FA2", athlete.FullName);
				Assert.Collection(athlete.Results, result =>
				{
					Assert.Equal(3, result.AthleteCourseId);
					Assert.Equal(4, result.RaceId);
					Assert.Equal("Test Race 4", result.RaceName);
					Assert.Equal(4, result.CourseId);
					Assert.Equal("Test Course 4", result.CourseName);
					Assert.Equal(21, result.OverallRank);
					Assert.Equal(11, result.GenderRank);
					Assert.Equal(6, result.DivisionRank);
					Assert.Equal("33:20", result.PaceWithTime.TimeFormatted);
				});

				Assert.Collection(athlete.Stats, stat =>
				{
					Assert.Equal("Mountain Biking", stat.RaceSeriesTypeName);
					Assert.Equal(1, stat.ActualTotal);
					Assert.Equal(5000, stat.TotalDistance);
				});
			});
		}
	}
}
