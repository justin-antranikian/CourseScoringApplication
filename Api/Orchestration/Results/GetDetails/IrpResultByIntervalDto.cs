using Api.DataModels;

namespace Api.Orchestration.Results.GetDetails;

public record IrpResultByIntervalDto
(
    string IntervalName,
    IntervalType IntervalType,
    bool IntervalFinished,
    PaceWithTime PaceWithTimeCumulative,
    PaceWithTime PaceWithTimeIntervalOnly,
    int? OverallRank,
    int? GenderRank,
    int? PrimaryDivisionRank,
    int OverallCount,
    int GenderCount,
    int PrimaryDivisionCount,
    BetweenIntervalTimeIndicator OverallIndicator,
    BetweenIntervalTimeIndicator GenderIndicator,
    BetweenIntervalTimeIndicator PrimaryDivisionIndicator,
    string? CrossingTime,
    bool IsFullCourse,
    string IntervalDescription,
    string? Percentile,
    double IntervalDistance,
    double CumulativeDistance
);
