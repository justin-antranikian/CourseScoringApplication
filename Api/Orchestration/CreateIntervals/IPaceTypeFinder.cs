using Api.DataModels.Enums;

namespace Api.Orchestration.CreateIntervals;

internal interface IPaceTypeFinder
{
    public PaceType GetPaceType(string intervalName);

    public IntervalType GetIntervalType(string intervalName);
}
