using Api.DataModels;
using Core;

namespace Api.Orchestration.CreateIntervals;

internal class SequentialIntervalPaceTypeFinder(Course course) : IPaceTypeFinder
{
    public PaceType GetPaceType(string intervalName) => course.PaceType;

    public IntervalType GetIntervalType(string intervalName)
    {
        return course.CourseType switch
        {
            CourseType.Running5K => IntervalType.Run,
            CourseType.Running10K => IntervalType.Run,
            CourseType.Running25K => IntervalType.Run,
            CourseType.HalfMarathon => IntervalType.Run,
            CourseType.Marathon => IntervalType.Run,
            CourseType.HalfCentryBikeRace => IntervalType.Bike,
            CourseType.CentryBikeRace => IntervalType.Bike,
            CourseType.MountainBikeRace => IntervalType.MountainBike,
            CourseType.DownhillBikeRace => IntervalType.MountainBike,
            CourseType.CrossCountrySkiing => IntervalType.CrossCountrySki,
            CourseType.FifteenHundredMeterSwim => IntervalType.Swim,
            CourseType.OneMileSwim => IntervalType.Swim,
            CourseType.TwoMileSwim => IntervalType.Swim,
            _ => throw new NotImplementedException(),
        };
    }
}
