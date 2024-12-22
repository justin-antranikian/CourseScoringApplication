using Api.Orchestration.Races.GetLeaderboard;

namespace ApiTests.Orchestration.GetLeaderboard.GetRaceLeaderboard;

public class GetRaceLeaderboardOrchestratorTests
{
    [Fact]
    public async Task GetLeaderboardDto_ReturnsCorrectResult()
    {
        var dbContext = ScoringDbContextCreator.GetScoringDbContext();

        var orchestrator = new GetRaceLeaderboardOrchestrator(dbContext);
        var leaderboardResult = await orchestrator.GetRaceLeaderboardDto(1);

        Assert.Collection(leaderboardResult.Leaderboards, result =>
        {
            Assert.Equal(2, result.CourseId);
            Assert.Equal("Course 2", result.CourseName);
            Assert.Collection(result.Results, athlete =>
            {
                Assert.Equal(1, athlete.AthleteId);
                Assert.Equal("FA LA", athlete.FullName);
                Assert.Equal(9, athlete.RaceAge);
                Assert.Equal("F", athlete.GenderAbbreviated);
                Assert.Equal("BA", athlete.Bib);

                Assert.Equal("11:40", athlete.PaceWithTimeCumulative.TimeFormatted);
                Assert.True(athlete.PaceWithTimeCumulative.HasPace);
                Assert.Equal("mph", athlete.PaceWithTimeCumulative.PaceLabel);
                Assert.Equal("3.2", athlete.PaceWithTimeCumulative.PaceValue);

                Assert.Equal(2, athlete.AthleteCourseId);
                Assert.Equal(1, athlete.OverallRank);
                Assert.Equal(1, athlete.GenderRank);
                Assert.Equal(1, athlete.DivisionRank);
            }, athlete =>
            {
                Assert.Equal(2, athlete.AthleteId);
                Assert.Equal(5, athlete.AthleteCourseId);
                Assert.Equal(2, athlete.OverallRank);
                Assert.Equal(2, athlete.GenderRank);
                Assert.Equal(2, athlete.DivisionRank);
            }, athlete =>
            {
                Assert.Equal(3, athlete.AthleteId);
                Assert.Equal(8, athlete.AthleteCourseId);
                Assert.Equal(3, athlete.OverallRank);
                Assert.Equal(3, athlete.GenderRank);
                Assert.Equal(3, athlete.DivisionRank);
            });
        }, result =>
        {
            Assert.Equal(1, result.CourseId);
            Assert.Equal("Course 1", result.CourseName);
            Assert.Equal("Bike", result.HighestIntervalName);
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
        });
    }
}
