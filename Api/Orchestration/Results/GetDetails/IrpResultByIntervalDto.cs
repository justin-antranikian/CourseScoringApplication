namespace Api.Orchestration.Results.GetDetails;

public record IrpResultByIntervalDto
{
    public required string IntervalName { get; init; }
    public required PaceWithTime? PaceWithTimeCumulative { get; init; }
    public required PaceWithTime? PaceWithTimeIntervalOnly { get; init; }
    public required int? OverallRank { get; init; }
    public required int? GenderRank { get; init; }
    public required int? PrimaryDivisionRank { get; init; }
    public required int OverallCount { get; init; }
    public required int GenderCount { get; init; }
    public required int PrimaryDivisionCount { get; init; }
    public required BetweenIntervalTimeIndicator OverallIndicator { get; init; }
    public required BetweenIntervalTimeIndicator GenderIndicator { get; init; }
    public required BetweenIntervalTimeIndicator PrimaryDivisionIndicator { get; init; }
    public required string? CrossingTime { get; init; }
    public required bool IsFullCourse { get; init; }
}
