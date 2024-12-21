using Api.DataModels;
using Core;

namespace Api.Orchestration.GetIrp;

public static class IrpResultByIntervalDtoMapper
{
    public static IrpResultByIntervalDto GetIrpResultByIntervalDto(Course course, Interval interval, Result primaryDivisionResult, IrpResultByIntervalDto? previousInterval, MetadataGetTotalHelper metadataHelper)
    {
        var intervalFinished = primaryDivisionResult != null;
        var overallRank = primaryDivisionResult?.OverallRank;
        var genderRank = primaryDivisionResult?.GenderRank;
        var divisionRank = primaryDivisionResult?.DivisionRank;

        (string?, string?) GetCrossingTimeAndPercentile()
        {
            if (primaryDivisionResult is null)
            {
                return (null, null);
            }

            var crossingTime = course.GetCrossingTime(primaryDivisionResult.TimeOnCourse);
            var percentile = PercentileHelper.GetPercentile(overallRank.Value, metadataHelper.GetOverallTotal());
            return (crossingTime, percentile);
        }

        var (crossingTime, percentile) = GetCrossingTimeAndPercentile();

        var resultByIntervalDto = new IrpResultByIntervalDto
        (
            interval.Name,
            interval.IntervalType,
            intervalFinished,
            GetCumulativePaceTime(course, interval, primaryDivisionResult),
            GetIntervalPaceTime(course, interval, primaryDivisionResult),
            overallRank,
            genderRank,
            divisionRank,
            metadataHelper.GetOverallTotal(),
            metadataHelper.GetGenderTotal(),
            metadataHelper.GetPrimaryDivisionTotal(),
            GetIndicator(previousInterval, previousInterval?.OverallRank, overallRank),
            GetIndicator(previousInterval, previousInterval?.GenderRank, genderRank),
            GetIndicator(previousInterval, previousInterval?.PrimaryDivisionRank, divisionRank),
            crossingTime,
            interval.IsFullCourse,
            interval.Description,
            percentile,
            interval.Distance,
            interval.DistanceFromStart
        );

        return resultByIntervalDto;
    }

    private static PaceWithTime? GetCumulativePaceTime(Course course, Interval interval, Result result)
    {
        return result != null ? course.GetPaceWithTime(result.TimeOnCourse, interval.DistanceFromStart) : null;
    }

    private static PaceWithTime GetIntervalPaceTime(Course course, Interval interval, Result result)
    {
        if (result == null)
        {
            return null;
        }

        return PaceCalculator.GetPaceWithTime(interval.PaceType, course.PreferedMetric, result.TimeOnInterval, interval.Distance);
    }

    private static BetweenIntervalTimeIndicator GetIndicator(IrpResultByIntervalDto previous, int? previousRank, int? currentRank)
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
