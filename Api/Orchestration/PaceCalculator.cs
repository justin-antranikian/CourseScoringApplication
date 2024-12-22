using Api.DataModels;

namespace Api.Orchestration;

public static class PaceCalculator
{
    public static PaceWithTime GetPaceWithTime(PaceType paceType, PreferredMetric preferredMetric, int timeInSeconds, double distanceInMeters)
    {
        var timeFormatted = TimeFormatter.Format(timeInSeconds);
        if (paceType == PaceType.None)
        {
            return new PaceWithTime(timeFormatted, false);
        }

        var paceFormatted = GetPaceFormatted(paceType, preferredMetric, timeInSeconds, distanceInMeters);
        var caption = GetCaption(paceType, preferredMetric);
        return new PaceWithTime(timeFormatted, true, paceFormatted, caption);
    }

    public static string GetPaceFormatted(PaceType paceType, PreferredMetric preferedMetric, int timeInSeconds, double distanceInMeters)
    {
        var paceAsDouble = GetPace(paceType, preferedMetric, timeInSeconds, distanceInMeters);
            
        if (paceType == PaceType.MilesOrKilometersPerHour)
        {
            return paceAsDouble.ToString();
        }

        var seconds = (int)(paceAsDouble % 1 * 60);
        var minutesAsSeconds = (int)(paceAsDouble) * 60;
        return TimeFormatter.Format(minutesAsSeconds + seconds);
    }

    public static double? GetPace(PaceType paceType, PreferredMetric preferredMetric, int timeInSeconds, double distanceInMeters)
    {
        if (paceType == PaceType.None)
        {
            return null;
        }

        var useImperial = preferredMetric == PreferredMetric.Imperial;

        if (paceType == PaceType.MilesOrKilometersPerHour)
        {
            var distance = GetDistance(distanceInMeters, useImperial);
            var hours = timeInSeconds / 3600.0;
            var raw = distance / hours;
            return Math.Round(raw, 2);
        }

        var minutes = timeInSeconds / 60.0;

        if (paceType == PaceType.MinuteMileOrKilometer)
        {
            var distance = GetDistance(distanceInMeters, useImperial);
            var raw = minutes / distance;
            return Math.Round(raw, 2);
        }

        if (paceType == PaceType.MinutePer100Meters)
        {
            var distance = distanceInMeters / 100.0;
            var raw = minutes / distance;
            return Math.Round(raw, 2);
        }

        return null;
    }

    private static double GetDistance(double distanceInMeters, bool useImperial)
    {
        var distance = useImperial ? distanceInMeters * 0.00062137 : distanceInMeters / 1000;
        return distance;
    }

    public static string? GetCaption(PaceType paceType, PreferredMetric preferredMetric)
    {
        var result = (paceType, preferedMetric: preferredMetric) switch
        {
            (PaceType.None, _) => null,
            (PaceType.MinutePer100Meters, PreferredMetric.Imperial) => "min/100m",
            (PaceType.MinutePer100Meters, PreferredMetric.Metric) => "min/100m",
            (PaceType.MilesOrKilometersPerHour, PreferredMetric.Imperial) => "mph",
            (PaceType.MilesOrKilometersPerHour, PreferredMetric.Metric) => "kph",
            (PaceType.MinuteMileOrKilometer, PreferredMetric.Imperial) => "min/mi",
            (PaceType.MinuteMileOrKilometer, PreferredMetric.Metric) => "min/km",
            _ => null
        };

        return result;
    }
}
