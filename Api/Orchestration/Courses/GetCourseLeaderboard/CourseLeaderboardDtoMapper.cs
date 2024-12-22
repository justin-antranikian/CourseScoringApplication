using Api.DataModels;

namespace Api.Orchestration.Courses.GetCourseLeaderboard;

public static class CourseLeaderboardDtoMapper
{
    public static CourseLeaderboardDto GetCourseLeaderboardDto(Course course, Race race, List<CourseLeaderboardByIntervalDto> leaderboards)
    {
        var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(course.StartDate);
        var raceSeries = race.RaceSeries;

        var courseLeaderboardDto = new CourseLeaderboardDto
        {
            CourseDate = dateFormatted,
            CourseDistance = course.Distance,
            CourseName = course.Name,
            CourseTime = timeFormatted,
            Leaderboards = leaderboards,
            LocationInfoWithRank = new LocationInfoWithRank(raceSeries),
            RaceId = race.Id,
            RaceName = race.Name,
            RaceSeriesDescription = raceSeries.Description,
            RaceSeriesId = race.Id,
            RaceSeriesType = raceSeries.RaceSeriesType,
            RaceSeriesTypeName = raceSeries.RaceSeriesType.ToString(),
        };

        return courseLeaderboardDto;
    }
}
