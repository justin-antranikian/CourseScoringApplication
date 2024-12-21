using Api.DataModels;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.CreateIntervals;

public class CreateIntervalsOrchestrator
{
    private readonly ScoringDbContext _scoringDbContext;

    public CreateIntervalsOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task Create()
    {
        var courses = await _scoringDbContext.Courses.ToListAsync();
        var courseIntervals = courses.SelectMany(GetIntervals);

        await _scoringDbContext.Intervals.AddRangeAsync(courseIntervals);
        await _scoringDbContext.SaveChangesAsync();
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
        var possibleDescriptions = new[]
        {
            "This is a really tough part of the event.",
            "You are part of the way through. You need to keep going.",
            "This interval will set you up for success later in the event.",
            "Might be a good idea to conserve some energy on this interval.",
            "The full course. You have finished the event !"
        };

        var cumulativeDistance = distancePerInterval;
        var intervalOrder = 1;
        var intervals = intervalNames.Select(intervalName =>
        {
            var intervalType = paceTypeFinder.GetIntervalType(intervalName);
            var paceType = paceTypeFinder.GetPaceType(intervalName);
            var interval = new Interval(course.Id, intervalName, distancePerInterval, cumulativeDistance, intervalOrder, false, paceType, intervalType, possibleDescriptions.GetRandomValue());
            cumulativeDistance += distancePerInterval;
            intervalOrder++;
            return interval;
        }).ToList();

        var fullCourseDistance = intervals.Max(oo => oo.DistanceFromStart);
        var fullCourseDescription = "Congrats, you have finished the event. Now enjoy the reward of completing the full course.";
        var fullCourse = new Interval(course.Id, "Full Course", fullCourseDistance, fullCourseDistance, intervalOrder, true, course.PaceType, IntervalType.FullCourse, fullCourseDescription);
        course.Distance = fullCourseDistance;
        return intervals.ConcatSingle(fullCourse);
    }
}
