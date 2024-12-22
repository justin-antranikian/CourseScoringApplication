using Api.DataModels;
using Api.Orchestration.Courses.GetCourseLeaderboard;

namespace ApiTests.Orchestration.GetLeaderboard.GetCourseLeaderboard;

public class CourseLeaderboardDtoTests
{
    [Fact]
    public void CourseLeaderboardDtoMapper_MapsAllFields()
    {
        var course = new Course
        {
            Id = 1,
            Name = "CA",
            RaceId = 1,
            StartDate = new DateTime(2010, 1, 1, 6, 30, 0),
            Distance = 1000
        };

        var race = new Race
        {
            Id = 1,
            Name = "RA",
            RaceSeriesId = 1,
            RaceSeries = new()
            {
                Description = "RD",
                RaceSeriesType = RaceSeriesType.Running,
                State = "SA",
                Area = "AA",
                City = "CA"
            }
        };

        var courseLeaderboardDto = CourseLeaderboardDtoMapper.GetCourseLeaderboardDto(course, race, new());

        Assert.Equal(1, courseLeaderboardDto.RaceId);
        Assert.Equal("RA", courseLeaderboardDto.RaceName);
        Assert.Equal(1, courseLeaderboardDto.RaceSeriesId);
        Assert.Equal("RD", courseLeaderboardDto.RaceSeriesDescription);
        Assert.Equal(RaceSeriesType.Running, courseLeaderboardDto.RaceSeriesType);
        Assert.NotNull(courseLeaderboardDto.LocationInfoWithRank);

        Assert.Equal("CA", courseLeaderboardDto.CourseName);
        Assert.Equal("1/1/2010", courseLeaderboardDto.CourseDate);
        Assert.Equal("06:30:00 AM", courseLeaderboardDto.CourseTime);
        Assert.Equal(1000, courseLeaderboardDto.CourseDistance);
        Assert.Empty(courseLeaderboardDto.Leaderboards);
    }
}
