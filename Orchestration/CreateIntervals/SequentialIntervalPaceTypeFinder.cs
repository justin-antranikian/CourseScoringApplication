using Core;
using DataModels;

namespace Orchestration.CreateIntervals
{
	internal class SequentialIntervalPaceTypeFinder : IPaceTypeFinder
	{
		private readonly Course _course;

		public SequentialIntervalPaceTypeFinder(Course course)
		{
			_course = course;
		}

		public PaceType GetPaceType(string intervalName) => _course.PaceType;

		public IntervalType GetIntervalType(string intervalName)
		{
			return _course.CourseType switch
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
				_ => throw new System.NotImplementedException(),
			};
		}
	}
}
