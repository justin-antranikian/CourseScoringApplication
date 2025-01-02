namespace Api.Orchestration.Results.Compare;

public record ResultCompareIntervalDto
{
    public required string? CrossingTime { get; init; }
    public required string IntervalName { get; init; }
    public required PaceWithTime? PaceWithTime { get; init; }
    public required int? OverallRank { get; init; }
    public required int? GenderRank { get; init; }
    public required int? PrimaryDivisionRank { get; init; }
}
