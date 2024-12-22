using Api.DataModels;
using Api.Orchestration;

namespace ApiTests.Orchestration.GetLeaderboard;

public class LeaderboardResultDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var leaderboardDto = GetLeaderboardResultDto();

        Assert.Equal(1, leaderboardDto.AthleteId);
        Assert.Equal("FA LA", leaderboardDto.FullName);
        Assert.Equal(10, leaderboardDto.RaceAge);
        Assert.Equal("M", leaderboardDto.GenderAbbreviated);
        Assert.Equal("BA", leaderboardDto.Bib);

        Assert.Equal("01:01", leaderboardDto.PaceWithTimeCumulative.TimeFormatted);
        Assert.False(leaderboardDto.PaceWithTimeCumulative.HasPace);
        Assert.Null(leaderboardDto.PaceWithTimeCumulative.PaceLabel);
        Assert.Null(leaderboardDto.PaceWithTimeCumulative.PaceValue);

        Assert.Equal(1, leaderboardDto.AthleteCourseId);
        Assert.Equal(153, leaderboardDto.OverallRank);
        Assert.Equal(154, leaderboardDto.GenderRank);
        Assert.Equal(155, leaderboardDto.DivisionRank);
    }

    #region test preperation methods

    private static LeaderboardResultDto GetLeaderboardResultDto()
    {
        var result = new Result
        {
            AthleteCourseId = 1,
            CourseId = 5,
            IntervalId = 6,
            BracketId = 7,
            TimeOnInterval = 150,
            TimeOnCourse = 151,
            Rank = 152,
            OverallRank = 153,
            GenderRank = 154,
            DivisionRank = 155,
            AthleteCourse = new AthleteCourse { Bib = "BA" }
        };

        var athlete = new Athlete
        {
            Id = 1,
            FirstName = "FA",
            LastName = "LA",
            FullName = "FA LA",
            Gender = Gender.Male,
            City = "CA",
            State = "SA",
            DateOfBirth = new DateTime(2000, 1, 1)
        };

        var course = new Course
        {
            Id = 1,
            StartDate = new DateTime(2010, 1, 1)
        };

        return LeaderboardResultDtoMapper.GetLeaderboardResultDto(result, athlete, new PaceWithTime("01:01", false), course);
    }

    #endregion
}
