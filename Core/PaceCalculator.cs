using System;

namespace Core;

public static class PaceCalculator
{
    public static PaceWithTime GetPaceWithTime(PaceType paceType, PreferedMetric preferedMetric, int timeInSeconds, double distanceInMeters)
    {
        var timeFormatted = TimeFormatter.Format(timeInSeconds);
        if (paceType == PaceType.None)
        {
            return new PaceWithTime(timeFormatted, false);
        }

        var paceFormatted = GetPaceFormatted(paceType, preferedMetric, timeInSeconds, distanceInMeters);
        var caption = GetCaption(paceType, preferedMetric);
        return new PaceWithTime(timeFormatted, true, paceFormatted, caption);
    }

    public static string GetPaceFormatted(PaceType paceType, PreferedMetric preferedMetric, int timeInSeconds, double distanceInMeters)
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

    public static double? GetPace(PaceType paceType, PreferedMetric preferedMetric, int timeInSeconds, double distanceInMeters)
    {
        if (paceType == PaceType.None)
        {
            return null;
        }

        var useImperial = preferedMetric == PreferedMetric.Imperial;

        if (paceType == PaceType.MilesOrKilometersPerHour)
        {
            var distance = useImperial ? distanceInMeters * 0.00062137 : distanceInMeters / 1000;
            var hours = timeInSeconds / 3600.0;
            var raw = distance / hours;
            return Math.Round(raw, 2);
        }

        var minutes = timeInSeconds / 60.0;

        if (paceType == PaceType.MinuteMileOrKilometer)
        {
            var distance = useImperial ? distanceInMeters * 0.00062137 : distanceInMeters / 1000;
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

    public static string? GetCaption(PaceType paceType, PreferedMetric preferedMetric)
    {
        var result = (paceType, preferedMetric) switch
        {
            (PaceType.None, _) => null,
            (PaceType.MinutePer100Meters, PreferedMetric.Imperial) => "min/100m",
            (PaceType.MinutePer100Meters, PreferedMetric.Metric) => "min/100m",
            (PaceType.MilesOrKilometersPerHour, PreferedMetric.Imperial) => "mph",
            (PaceType.MilesOrKilometersPerHour, PreferedMetric.Metric) => "kph",
            (PaceType.MinuteMileOrKilometer, PreferedMetric.Imperial) => "min/mi",
            (PaceType.MinuteMileOrKilometer, PreferedMetric.Metric) => "min/km",
            _ => null
        };

        return result;
    }
}
