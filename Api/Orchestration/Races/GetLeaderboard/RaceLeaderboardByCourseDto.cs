namespace Api.Orchestration.Races.GetLeaderboard;

public record RaceLeaderboardByCourseDto
{
    public required int CourseId { get; init; }

    public required string CourseName { get; init; }
    public required int SortOrder { get; init; }

    public required List<LeaderboardResultDto> Results { get; init; }
}
