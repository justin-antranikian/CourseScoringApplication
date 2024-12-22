using Api.DataModels;

namespace Api.Orchestration.GetLeaderboard.GetCourseLeaderboard;

public record CourseLeaderboardByIntervalDto
{
    public required string IntervalName { get; init; }
    public required IntervalType IntervalType { get; init; }
    public required List<LeaderboardResultDto> Results { get; init; }
    public required int TotalRacers { get; init; }
}
