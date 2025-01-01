using Api.DataModels;

namespace Api.Orchestration.Results.GetDetails;

public static class IrpResultByIntervalDtoMapper
{
    public static IrpResultByIntervalDto GetIrpResultByIntervalDto(Course course, Interval interval, Result? primaryDivisionResult, IrpResultByIntervalDto? previousInterval, MetadataGetTotalHelper metadataHelper)
    {
        var overallRank = primaryDivisionResult?.OverallRank;
        var genderRank = primaryDivisionResult?.GenderRank;
        var divisionRank = primaryDivisionResult?.DivisionRank;

        return new IrpResultByIntervalDto
        {
            CrossingTime = primaryDivisionResult != null ? course.GetCrossingTime(primaryDivisionResult.TimeOnCourse) : null,
            GenderCount = metadataHelper.GetGenderTotal(),
            GenderIndicator = GetIndicator(previousInterval, previousInterval?.GenderRank, genderRank),
            GenderRank = genderRank,
            IntervalName = interval.Name,
            IsFullCourse = interval.IsFullCourse,
            OverallCount = metadataHelper.GetOverallTotal(),
            OverallIndicator = GetIndicator(previousInterval, previousInterval?.OverallRank, overallRank),
            OverallRank = overallRank,
            PaceWithTimeCumulative = primaryDivisionResult != null ? course.GetPaceWithTime(primaryDivisionResult.TimeOnCourse, interval.DistanceFromStart) : null,
            PaceWithTimeIntervalOnly = primaryDivisionResult != null ? PaceCalculator.GetPaceWithTime(interval.PaceType, course.PreferedMetric, primaryDivisionResult.TimeOnInterval, interval.Distance) : null,
            PrimaryDivisionCount = metadataHelper.GetPrimaryDivisionTotal(),
            PrimaryDivisionIndicator = GetIndicator(previousInterval, previousInterval?.PrimaryDivisionRank, divisionRank),
            PrimaryDivisionRank = divisionRank,
        };
    }

    private static BetweenIntervalTimeIndicator GetIndicator(IrpResultByIntervalDto? previous, int? previousRank, int? currentRank)
    {
        if (!currentRank.HasValue)
        {
            return BetweenIntervalTimeIndicator.NotStarted;
        }

        if (previous is null || !previousRank.HasValue || previousRank == currentRank)
        {
            return BetweenIntervalTimeIndicator.StartingOrSame;
        }

        return currentRank < previousRank ? BetweenIntervalTimeIndicator.Improving : BetweenIntervalTimeIndicator.GettingWorse;
    }
}

public record IrpResultByIntervalDto
{
    public required string? CrossingTime { get; init; }
    public required int GenderCount { get; init; }
    public required BetweenIntervalTimeIndicator GenderIndicator { get; init; }
    public required int? GenderRank { get; init; }
    public required string IntervalName { get; init; }
    public required bool IsFullCourse { get; init; }
    public required int OverallCount { get; init; }
    public required BetweenIntervalTimeIndicator OverallIndicator { get; init; }
    public required int? OverallRank { get; init; }
    public required PaceWithTime? PaceWithTimeCumulative { get; init; }
    public required PaceWithTime? PaceWithTimeIntervalOnly { get; init; }
    public required int PrimaryDivisionCount { get; init; }
    public required BetweenIntervalTimeIndicator PrimaryDivisionIndicator { get; init; }
    public required int? PrimaryDivisionRank { get; init; }
}
