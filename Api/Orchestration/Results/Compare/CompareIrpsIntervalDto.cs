namespace Api.Orchestration.Results.Compare;

public record CompareIrpsIntervalDto
(
    string IntervalName,
    PaceWithTime? PaceWithTime,
    string? CrossingTime,
    int? OverallRank,
    int? GenderRank,
    int? PrimaryDivisionRank
);
