using Core;

namespace DataModels.Extensions;

public static class CourseExtensions
{
    /// <summary>
    /// Shows the finishTime from when the course starts.
    /// So if the course starts at 7:30 and you had a time of 1000 this returns "7:46:40 AM"
    /// </summary>
    /// <param name="course"></param>
    /// <param name="timeInSeconds"></param>
    /// <returns></returns>
    public static string GetCrossingTime(this Course course, int timeInSeconds)
    {
        return DateTimeHelper.GetCrossingTime(course.StartDate, timeInSeconds);
    }

    public static PaceWithTime GetPaceWithTime(this Course course, int timeInSeconds, double? intervalDistance = null)
    {
        var distance = intervalDistance ?? course.Distance;
        return PaceCalculator.GetPaceWithTime(course.PaceType, course.PreferedMetric, timeInSeconds, distance);
    }
}
