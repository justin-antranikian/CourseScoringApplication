using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GenerateData;

public class CreateIntervalsOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task Create()
    {
        var courses = await scoringDbContext.Courses.ToListAsync();
        var courseIntervals = courses.SelectMany(GetIntervals);

        await scoringDbContext.Intervals.AddRangeAsync(courseIntervals);
        await scoringDbContext.SaveChangesAsync();
    }

    private List<Interval> GetIntervals(Course course)
    {
        return course.CourseType switch
        {
            CourseType.Running5K => GetSequentialIntervals(2, course, 1000),
            CourseType.Running10K => GetSequentialIntervals(3, course, 2000),
            CourseType.Running25K => GetSequentialIntervals(4, course, 4000),
            CourseType.HalfMarathon => GetSequentialIntervals(5, course, 1000),
            CourseType.Marathon => GetSequentialIntervals(10, course, 1000),
            CourseType.SprintTriathalon => GetTriathalonIntervals(course, 1000),
            CourseType.OlympicTriathalon => GetTriathalonIntervals(course, 2000),
            CourseType.HalfIronmanTriathalon => GetTriathalonIntervals(course, 3000),
            CourseType.IronmanTriathalon => GetTriathalonIntervals(course, 4000),
            CourseType.HalfCentryBikeRace => GetSequentialIntervals(4, course, 2500),
            CourseType.CentryBikeRace => GetSequentialIntervals(8, course, 2500),
            CourseType.CrossCountrySkiing => GetSequentialIntervals(2, course, 1000),
            CourseType.FifteenHundredMeterSwim => GetSequentialIntervals(3, course, 500),
            CourseType.OneMileSwim => GetSequentialIntervals(6, course, 750),
            CourseType.TwoMileSwim => GetSequentialIntervals(12, course, 750),
            _ => GetSequentialIntervals(4, course, 2500)
        };
    }

    private static List<Interval> GetSequentialIntervals(int intervalAmount, Course course, int distancePerInterval)
    {
        var intervalNames = Enumerable.Range(1, intervalAmount).Select(oo => $"Interval {oo}").ToArray();
        var paceTypeFinder = new SequentialIntervalPaceTypeFinder(course);
        return GenerateIntervals(intervalNames, course, distancePerInterval, paceTypeFinder);
    }

    private static List<Interval> GetTriathalonIntervals(Course course, int distancePerInterval)
    {
        var intervalNames = new[] { "Swim", "Transition 1", "Bike", "Transition 2", "Run" };
        var paceTypeFinder = new TriathalonPaceTypeFinder();
        return GenerateIntervals(intervalNames, course, distancePerInterval, paceTypeFinder);
    }

    private static List<Interval> GenerateIntervals(string[] intervalNames, Course course, int distancePerInterval, IPaceTypeFinder paceTypeFinder)
    {
        var cumulativeDistance = distancePerInterval;
        var intervalOrder = 1;
        var intervals = intervalNames.Select(intervalName =>
        {
            var intervalType = paceTypeFinder.GetIntervalType(intervalName);
            var paceType = paceTypeFinder.GetPaceType(intervalName);

            var interval = new Interval
            {
                CourseId = course.Id,
                Distance = distancePerInterval,
                DistanceFromStart = cumulativeDistance,
                IntervalType = intervalType,
                IsFullCourse = false,
                Name = intervalName,
                Order = intervalOrder,
                PaceType = paceType
            };

            cumulativeDistance += distancePerInterval;
            intervalOrder++;
            return interval;
        }).ToList();

        var fullCourse = new Interval
        {
            CourseId = course.Id,
            Distance = cumulativeDistance,
            DistanceFromStart = cumulativeDistance,
            IntervalType = IntervalType.FullCourse,
            IsFullCourse = true,
            Name = "Full Course",
            Order = intervalOrder,
            PaceType = course.PaceType
        };

        course.Distance = cumulativeDistance;
        return intervals.ConcatSingle(fullCourse);
    }
}
