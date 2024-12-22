using Api.DataModels;

namespace Api.Orchestration.GenerateData;

internal interface IPaceTypeFinder
{
    public PaceType GetPaceType(string intervalName);

    public IntervalType GetIntervalType(string intervalName);
}
