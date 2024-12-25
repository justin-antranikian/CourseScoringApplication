using Api.DataModels;

namespace Api.Orchestration.Courses.GetLeaderboard;

public static class CourseLeaderboardDtoMapper
{
    public static CourseLeaderboardDto GetCourseLeaderboardDto(Course course, Race race, List<CourseLeaderboardByIntervalDto> leaderboards)
    {
        var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(course.StartDate);
        var raceSeries = race.RaceSeries!;

        return new CourseLeaderboardDto
        {
            CourseDate = dateFormatted,
            CourseDistance = course.Distance,
            CourseName = course.Name,
            CourseTime = timeFormatted,
            Leaderboards = leaderboards,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            RaceId = race.Id,
            RaceName = race.Name,
            RaceSeriesDescription = raceSeries.Description,
            RaceSeriesId = race.Id,
            RaceSeriesType = raceSeries.RaceSeriesType,
            RaceSeriesTypeName = raceSeries.RaceSeriesType.ToString(),
        };
    }
}

public record CourseLeaderboardDto
{
    public required string CourseDate { get; init; }
    public required double CourseDistance { get; init; }
    public required string CourseName { get; init; }
    public required string CourseTime { get; init; }
    public required List<CourseLeaderboardByIntervalDto> Leaderboards { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required int RaceId { get; init; }
    public required string RaceName { get; init; }
    public required int RaceSeriesId { get; init; }
    public required string RaceSeriesDescription { get; init; }
    public required RaceSeriesType RaceSeriesType { get; init; }
    public required string RaceSeriesTypeName { get; init; }
}
