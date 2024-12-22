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
            IntervalName = interval.Name,
            PaceWithTimeCumulative = primaryDivisionResult != null ? course.GetPaceWithTime(primaryDivisionResult.TimeOnCourse, interval.DistanceFromStart) : null,
            PaceWithTimeIntervalOnly = primaryDivisionResult != null ? PaceCalculator.GetPaceWithTime(interval.PaceType, course.PreferedMetric, primaryDivisionResult.TimeOnInterval, interval.Distance) : null,
            OverallRank = overallRank,
            GenderRank = genderRank,
            PrimaryDivisionRank = divisionRank,
            OverallCount = metadataHelper.GetOverallTotal(),
            GenderCount = metadataHelper.GetGenderTotal(),
            PrimaryDivisionCount = metadataHelper.GetPrimaryDivisionTotal(),
            OverallIndicator = GetIndicator(previousInterval, previousInterval?.OverallRank, overallRank),
            GenderIndicator = GetIndicator(previousInterval, previousInterval?.GenderRank, genderRank),
            PrimaryDivisionIndicator = GetIndicator(previousInterval, previousInterval?.PrimaryDivisionRank, divisionRank),
            CrossingTime = primaryDivisionResult != null ? course.GetCrossingTime(primaryDivisionResult.TimeOnCourse) : null,
            IsFullCourse = interval.IsFullCourse,
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
