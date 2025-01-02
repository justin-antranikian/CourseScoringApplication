namespace Api.Orchestration.Athletes.GetDetails;

public record ArpResultDto
{
    public required int AthleteCourseId { get; init; }

    public required int CourseId { get; init; }
    public required string CourseName { get; init; }
    public required int GenderCount { get; init; }
    public required int GenderRank { get; init; }
    public required int OverallCount { get; init; }
    public required int OverallRank { get; init; }
    public required PaceWithTime PaceWithTimeCumulative { get; init; }
    public required int PrimaryDivisionCount { get; init; }
    public required int PrimaryDivisionRank { get; init; }
    public required int RaceId { get; init; }
    public required string RaceName { get; init; }
}
