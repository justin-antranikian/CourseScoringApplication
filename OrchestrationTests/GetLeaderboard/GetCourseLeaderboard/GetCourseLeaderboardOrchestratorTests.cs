using Core;
using Orchestration.GetLeaderboard.GetCourseLeaderboard;
using System.Threading.Tasks;
using Xunit;

namespace OrchestrationTests.GetCourseResults.GetCourseLeaderboard
{
	public class GetCourseLeaderboardOrchestratorTests
	{
		/// <summary>
		/// This calls the orchestrator with the defaults. It will grab the best results from all intervals for the overall bracket.
		/// The first assertion tests that all athlete fields are mapped correctly. Other tests like this don't need to do this again.
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task NoFilters_ReturnsCorrectResults()
		{
			var dbContext = ScoringDbContextCreator.GetScoringDbContext();

			var orchestrator = new GetCourseLeaderboardOrchestrator(dbContext);
			var courseResultDto = await orchestrator.GetCourseLeaderboardDto(1, null, null);

			Assert.Collection(courseResultDto.CourseMetadata.Intervals, result =>
			{
				Assert.Equal(1, result.Id);
				Assert.Equal("Swim", result.DisplayName);
			}, result =>
			{
				Assert.Equal(2, result.Id);
				Assert.Equal("Transition 1", result.DisplayName);
			}, result =>
			{
				Assert.Equal(3, result.Id);
				Assert.Equal("Bike", result.DisplayName);
			}, result =>
			{
				Assert.Equal(4, result.Id);
				Assert.Equal("Transition 2", result.DisplayName);
			}, result =>
			{
				Assert.Equal(5, result.Id);
				Assert.Equal("Run", result.DisplayName);
			}, result =>
			{
				Assert.Equal(6, result.Id);
				Assert.Equal("Full Course", result.DisplayName);
			});

			Assert.Collection(courseResultDto.CourseMetadata.Brackets, result =>
			{
				Assert.Equal(1, result.Id);
				Assert.Equal("Overall", result.DisplayName);
				Assert.Equal(BracketType.Overall, result.BracketType);
			}, result =>
			{
				Assert.Equal(2, result.Id);
				Assert.Equal("Female", result.DisplayName);
				Assert.Equal(BracketType.Gender, result.BracketType);
			}, result =>
			{
				Assert.Equal(3, result.Id);
				Assert.Equal("Male", result.DisplayName);
				Assert.Equal(BracketType.Gender, result.BracketType);
			}, result =>
			{
				Assert.Equal(5, result.Id);
				Assert.Equal("F20-30", result.DisplayName);
				Assert.Equal(BracketType.PrimaryDivision, result.BracketType);
			}, result =>
			{
				Assert.Equal(4, result.Id);
				Assert.Equal("M20-30", result.DisplayName);
				Assert.Equal(BracketType.PrimaryDivision, result.BracketType);
			}, result =>
			{
				Assert.Equal(7, result.Id);
				Assert.Equal("Elite", result.DisplayName);
				Assert.Equal(BracketType.NonPrimaryDivision, result.BracketType);
			}, result =>
			{
				Assert.Equal(6, result.Id);
				Assert.Equal("Florida 20-30", result.DisplayName);
				Assert.Equal(BracketType.NonPrimaryDivision, result.BracketType);
			});

			Assert.Collection(courseResultDto.CourseMetadata.Courses, result =>
			{
				Assert.Equal(1, result.Id);
				Assert.Equal("Course 1", result.DisplayName);
			}, result =>
			{
				Assert.Equal(2, result.Id);
				Assert.Equal("Course 2", result.DisplayName);
			});

			Assert.Collection(courseResultDto.Leaderboards, result =>
			{
				Assert.Equal("Bike", result.IntervalName);
				Assert.Equal(1, result.TotalRacers);
				Assert.Collection(result.Results, athlete =>
				{
					Assert.Equal(4, athlete.AthleteId);
					Assert.Equal("FD LD", athlete.FullName);
					Assert.Equal(10, athlete.RaceAge);
					Assert.Equal("F", athlete.GenderAbbreviated);
					Assert.Equal("BD", athlete.Bib);

					Assert.Equal("35:09", athlete.PaceWithTimeCumulative.TimeFormatted);
					Assert.False(athlete.PaceWithTimeCumulative.HasPace);
					Assert.Null(athlete.PaceWithTimeCumulative.PaceLabel);
					Assert.Null(athlete.PaceWithTimeCumulative.PaceValue);
					
					Assert.Equal(10, athlete.AthleteCourseId);
					Assert.Equal(1, athlete.OverallRank);
					Assert.Equal(1, athlete.GenderRank);
					Assert.Equal(1, athlete.DivisionRank);
				});
			}, result =>
			{
				Assert.Equal("Transition 1", result.IntervalName);
				Assert.Equal(4, result.TotalRacers);
				Assert.Collection(result.Results, athlete =>
				{
					Assert.Equal(1, athlete.AthleteId);
					Assert.Equal(2, athlete.OverallRank);
					Assert.Equal(1, athlete.GenderRank);
					Assert.Equal(1, athlete.DivisionRank);
				}, athlete =>
				{
					Assert.Equal(2, athlete.AthleteId);
					Assert.Equal(3, athlete.OverallRank);
					Assert.Equal(2, athlete.GenderRank);
					Assert.Equal(2, athlete.DivisionRank);
				}, athlete =>
				{
					Assert.Equal(3, athlete.AthleteId);
					Assert.Equal(4, athlete.OverallRank);
					Assert.Equal(2, athlete.GenderRank);
					Assert.Equal(2, athlete.DivisionRank);
				});
			});
		}

		[Fact]
		public async Task WithSpecificInterval()
		{
			var dbContext = ScoringDbContextCreator.GetScoringDbContext();

			var orchestrator = new GetCourseLeaderboardOrchestrator(dbContext);
			var courseResults = await orchestrator.GetCourseLeaderboardDto(1, null, 2);

			Assert.Collection(courseResults.Leaderboards, result =>
			{
				Assert.Equal("Transition 1", result.IntervalName);
				Assert.Equal(4, result.TotalRacers);
				Assert.Collection(result.Results, athlete =>
				{
					Assert.Equal(1, athlete.AthleteId);
					Assert.Equal(1, athlete.OverallRank);
					Assert.Equal(1, athlete.GenderRank);
					Assert.Equal(1, athlete.DivisionRank);
				}, athlete =>
				{
					Assert.Equal(2, athlete.AthleteId);
					Assert.Equal(2, athlete.OverallRank);
					Assert.Equal(2, athlete.GenderRank);
					Assert.Equal(2, athlete.DivisionRank);
				}, athlete =>
				{
					Assert.Equal(3, athlete.AthleteId);
					Assert.Equal(3, athlete.OverallRank);
					Assert.Equal(1, athlete.GenderRank);
					Assert.Equal(1, athlete.DivisionRank);
				}, athlete =>
				{
					Assert.Equal(4, athlete.AthleteId);
					Assert.Equal(4, athlete.OverallRank);
					Assert.Equal(2, athlete.GenderRank);
					Assert.Equal(2, athlete.DivisionRank);
				});
			});
		}

		[Fact]
		public async Task WithSpecificBracketAndInterval()
		{
			var dbContext = ScoringDbContextCreator.GetScoringDbContext();

			var orchestrator = new GetCourseLeaderboardOrchestrator(dbContext);
			var courseResults = await orchestrator.GetCourseLeaderboardDto(1, 7, 2);

			Assert.Collection(courseResults.Leaderboards, result =>
			{
				Assert.Equal("Transition 1", result.IntervalName);
				Assert.Equal(2, result.TotalRacers);
				Assert.Collection(result.Results, athlete =>
				{
					Assert.Equal(4, athlete.AthleteCourseId);
					Assert.Equal(2, athlete.OverallRank);
					Assert.Equal(2, athlete.GenderRank);
					Assert.Equal(1, athlete.DivisionRank);
				}, athlete =>
				{
					Assert.Equal(10, athlete.AthleteCourseId);
					Assert.Equal(4, athlete.OverallRank);
					Assert.Equal(2, athlete.GenderRank);
					Assert.Equal(2, athlete.DivisionRank);
				});
			});
		}

		[Fact]
		public async Task WithPagination()
		{
			var dbContext = ScoringDbContextCreator.GetScoringDbContext();

			var orchestrator = new GetCourseLeaderboardOrchestrator(dbContext);
			var courseResults = await orchestrator.GetCourseLeaderboardDto(1, 1, 1, 2, 2);

			Assert.Collection(courseResults.Leaderboards, result =>
			{
				Assert.Equal("Swim", result.IntervalName);
				Assert.Equal(4, result.TotalRacers);
				Assert.Collection(result.Results, athlete =>
				{
					Assert.Equal(2, athlete.AthleteId);
					Assert.Equal(2, athlete.OverallRank);
					Assert.Equal(2, athlete.GenderRank);
					Assert.Equal(2, athlete.DivisionRank);
				}, athlete =>
				{
					Assert.Equal(3, athlete.AthleteId);
					Assert.Equal(3, athlete.OverallRank);
					Assert.Equal(1, athlete.GenderRank);
					Assert.Equal(1, athlete.DivisionRank);
				});
			});
		}
	}
}
