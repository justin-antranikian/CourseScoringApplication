using Api.DataModels;

namespace Api.Orchestration.Courses.GetLeaderboard;

public record CourseLeaderboard
{
    public required string IntervalName { get; init; }
    public required IntervalType IntervalType { get; init; }
    public required List<LeaderboardResultDto> Results { get; init; }
    public required int TotalRacers { get; init; }
}
