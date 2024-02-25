namespace Orchestration.GetLeaderboard.GetCourseLeaderboard;

/// <summary>
/// For a course leaderboard we split the results up by interval.
/// </summary>
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
}
