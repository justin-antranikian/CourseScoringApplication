namespace Orchestration.GetLeaderboard.GetCourseLeaderboard;

public static class CourseLeaderboardDtoMapper
{
    public static CourseLeaderboardDto GetCourseLeaderboardDto(Course course, Race race, CourseMetadata courseMetadata, List<CourseLeaderboardByIntervalDto> leaderboards)
    {
        var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(course.StartDate);
        var raceSeries = race.RaceSeries;

        var courseLeaderboardDto = new CourseLeaderboardDto
        {
            CourseDate = dateFormatted,
            CourseDistance = course.Distance,
            CourseMetadata = courseMetadata,
            CourseName = course.Name,
            CourseTime = timeFormatted,
            Leaderboards = leaderboards,
            LocationInfoWithRank = new LocationInfoWithRank(raceSeries),
            RaceId = race.Id,
            RaceName = race.Name,
            RaceSeriesDescription = raceSeries.Description,
            RaceSeriesId = race.Id,
            RaceSeriesType = raceSeries.RaceSeriesType
        };

        return courseLeaderboardDto;
    }
}
