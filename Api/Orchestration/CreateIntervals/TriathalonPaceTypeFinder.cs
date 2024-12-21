using Core;

namespace Api.Orchestration.CreateIntervals;

internal class TriathalonPaceTypeFinder : IPaceTypeFinder
{
    public PaceType GetPaceType(string intervalName)
    {
        return intervalName switch
        {
            "Swim" => PaceType.MinutePer100Meters,
            "Bike" => PaceType.MilesOrKilometersPerHour,
            "Run" => PaceType.MinuteMileOrKilometer,
            _ => PaceType.None
        };
    }

    public IntervalType GetIntervalType(string intervalName)
    {
        return intervalName switch
        {
            "Swim" => IntervalType.Swim,
            "Bike" => IntervalType.Bike,
            "Run" => IntervalType.Run,
            "Transition 1" => IntervalType.Transition,
            "Transition 2" => IntervalType.Transition,
            _ => throw new NotImplementedException(),
        };
    }
}
