using DataModels;
using Orchestration.GetCompetetorsForIrp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.GetCompetetorsForIrp;

public class GetCompetetorsForIrpOrchestratorTests
{
	[Fact]
	public async Task MapsAllFields()
	{
		var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

		var athleteCourseBrackets = new List<AthleteCourseBracket>
		{
			new (1, 1, 1)
			{
				Id = 10,
				Bracket = new() { BracketType = Core.BracketType.Overall }
			},
			new (1, 1, 2)
			{
				Id = 20,
				Bracket = new() { BracketType = Core.BracketType.Gender }
			},
			new (1, 1, 3)
			{
				Id = 30,
				Bracket = new() { BracketType = Core.BracketType.PrimaryDivision }
			},
			new (1, 1, 4)
			{
				Id = 40,
				Bracket = new() { BracketType = Core.BracketType.NonPrimaryDivision }
			},
			new (2, 1, 1)
			{
				Id = 50,
				Bracket = new() { BracketType = Core.BracketType.Overall }
			}
		};

		var intervals = new List<Interval>
		{
			new() { Id = 1, Name = "Interval 1", Order = 1 },
			new() { Id = 2, Name = "Interval 2", Order = 2 },
			new() { Id = 3, Name = "Interval 3", Order = 3 },
			new() { Id = 4, Name = "Full Course", Order = 4, IsFullCourse = true },
		};

		var course = new Course { Id = 1 };

		var athleteCourses = Enumerable.Range(2, 50).Select(static index =>
		{
			return new AthleteCourse
			{
				Id = index,
				Athlete = new() { Id = index, FullName = $"JA{index}" },
				CourseId = 1,
			};
		}).ToList();

		var mainAthleteCourse = new AthleteCourse
		{
			Id = 1,
			Athlete = new() { Id = 1, FullName = "Justin Antranikian Full Name" },
			CourseId = 1
		};

		var baseResult = new Result { AthleteCourseId = 1, BracketId = 1, Rank = 99, IntervalId = 1, IsHighestIntervalCompleted = false, AthleteCourse = mainAthleteCourse };

		var results = new List<Result>
		{
			baseResult,
			baseResult with { BracketId = 2, Rank = 49 },
			baseResult with { BracketId = 3, Rank = 24 }
		};

		results.AddRange(GetResultHighestIntervalResults(1, 25, mainAthleteCourse));
		results.AddRange(GetResultHighestIntervalResults(2, 26, athleteCourses[0]));
		results.AddRange(GetResultHighestIntervalResults(3, 27, athleteCourses[1]));
		results.AddRange(GetResultHighestIntervalResults(4, 28, athleteCourses[2]));
		results.AddRange(GetResultHighestIntervalResults(5, 24, athleteCourses[3]));
		results.AddRange(GetResultHighestIntervalResults(6, 23, athleteCourses[4]));
		results.AddRange(GetResultHighestIntervalResults(7, 22, athleteCourses[5]));
		results.AddRange(GetResultHighestIntervalResults(8, 21, athleteCourses[6]));
		results.AddRange(GetResultHighestIntervalResults(9, 29, athleteCourses[7]));

		await dbContext.AthleteCourses.AddAsync(mainAthleteCourse);
		await dbContext.AtheleteCourseBrackets.AddRangeAsync(athleteCourseBrackets);
		await dbContext.Results.AddRangeAsync(results);
		await dbContext.Courses.AddAsync(course);
		await dbContext.Intervals.AddRangeAsync(intervals);

		await dbContext.SaveChangesAsync();

		var orchestrator = new GetCompetetorsForIrpOrchestrator(dbContext);
		var competetorsForIrpResult = await orchestrator.GetCompetetorsForIrpResult(1);

		Assert.Collection(competetorsForIrpResult.FasterAthletes, result =>
		{
			Assert.Equal(5, result.AthleteId);
			Assert.Equal("JA5", result.FullName);
		}, result =>
		{
			Assert.Equal(6, result.AthleteId);
			Assert.Equal("JA6", result.FullName);
		}, result =>
		{
			Assert.Equal(7, result.AthleteId);
			Assert.Equal("JA7", result.FullName);
		});

		Assert.Collection(competetorsForIrpResult.SlowerAthletes, result =>
		{
			Assert.Equal(2, result.AthleteId);
			Assert.Equal("JA2", result.FullName);
		}, result =>
		{
			Assert.Equal(3, result.AthleteId);
			Assert.Equal("JA3", result.FullName);
		}, result =>
		{
			Assert.Equal(4, result.AthleteId);
			Assert.Equal("JA4", result.FullName);
		});
	}

	private static List<Result> GetResultHighestIntervalResults(int atheleteCourseId, int primaryRank, AthleteCourse athleteCourse)
	{
		var baseResult = new Result { AthleteCourseId = atheleteCourseId, IntervalId = 2, IsHighestIntervalCompleted = true, AthleteCourse = athleteCourse };

		return new()
		{
			baseResult with { BracketId = 1, Rank = primaryRank + 75 },
			baseResult with { BracketId = 2, Rank = primaryRank + 25 },
			baseResult with { BracketId = 3, Rank = primaryRank },
		};
	}
}
