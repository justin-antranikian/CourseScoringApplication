using Bogus;
using Core.Enums;

namespace Orchestration.GenerateData;

public static class CourseGenerator
{
    private static readonly Dictionary<RaceSeriesType, CourseType[]> _raceAndCourseTypeDictionary = new()
    {
        { RaceSeriesType.Triathalon, new [] { CourseType.SprintTriathalon, CourseType.OlympicTriathalon, CourseType.HalfIronmanTriathalon, CourseType.IronmanTriathalon } },
        { RaceSeriesType.Running, new [] { CourseType.Running5K, CourseType.Running10K, CourseType.Running25K, CourseType.HalfMarathon, CourseType.Marathon } },
        { RaceSeriesType.RoadBiking, new [] { CourseType.HalfCentryBikeRace, CourseType.CentryBikeRace } },
        { RaceSeriesType.MountainBiking, new [] { CourseType.MountainBikeRace, CourseType.DownhillBikeRace } },
        { RaceSeriesType.CrossCountrySkiing, new [] { CourseType.CrossCountrySkiing } },
        { RaceSeriesType.Swim, new [] { CourseType.FifteenHundredMeterSwim, CourseType.OneMileSwim, CourseType.TwoMileSwim } },
    };

    private static readonly Dictionary<RaceSeriesType, PaceType> _racePaceTypeDictionary = new()
    {
        { RaceSeriesType.Triathalon, PaceType.None },
        { RaceSeriesType.Running, PaceType.MinuteMileOrKilometer },
        { RaceSeriesType.RoadBiking, PaceType.MilesOrKilometersPerHour },
        { RaceSeriesType.MountainBiking, PaceType.MilesOrKilometersPerHour },
        { RaceSeriesType.CrossCountrySkiing, PaceType.MinuteMileOrKilometer },
        { RaceSeriesType.Swim, PaceType.MinutePer100Meters },
    };

    public static IEnumerable<Course> GetCourses(List<Race> races)
    {
        var courses = new List<Course>();

        foreach (var race in races)
        {
            var raceSeriesType = race.RaceSeries.RaceSeriesType;
            var possibleCourseTypes = _raceAndCourseTypeDictionary[raceSeriesType];

            var courseFaker = new Faker<Course>()
                .RuleFor(oo => oo.PreferedMetric, f => typeof(PreferredMetric).GetRandomEnumValue())
                .RuleFor(oo => oo.CourseType, f => possibleCourseTypes.GetRandomEnumValue()
            );

            var coursesForRace = courseFaker.Generate(3).Select((oo, index) => GetCourse(oo, index, race));
            courses.AddRange(coursesForRace);
        }

        return courses;
    }

    private static Course GetCourse(Course oo, int index, Race race)
    {
        var raceSeriesType = race.RaceSeries.RaceSeriesType;

        var descriptionEntries = new CourseInformationEntry[]
        {
            new() { CourseInformationType = CourseInformationType.Description, Description = "The most difficult course of the series." },
            new() { CourseInformationType = CourseInformationType.Description, Description = "This course is a great intro to the sport." },
            new() { CourseInformationType = CourseInformationType.Description, Description = "This is an intermediate level course." },
        };

        var motivationalEntries = new CourseInformationEntry[]
        {
            new() { CourseInformationType = CourseInformationType.Promotional, Description = "Train up for this event and come see us." },
            new() { CourseInformationType = CourseInformationType.Promotional, Description = "Help race for a cause." },
            new() { CourseInformationType = CourseInformationType.Promotional, Description = "Train up for this event and come see us." },
        };

        var howToPrepareEntries = new CourseInformationEntry[]
        {
            new() { CourseInformationType = CourseInformationType.HowToPrepare, Description = "A wetsuit is a good idea." },
            new() { CourseInformationType = CourseInformationType.HowToPrepare, Description = "This is an intense race through the desert. Bring a lot of water." },
            new() { CourseInformationType = CourseInformationType.HowToPrepare, Description = "There will not be any cell coverage. GPS is recommended." },
        };

        var courseInfos = (new[] { descriptionEntries, motivationalEntries, howToPrepareEntries }).SelectMany(oo => oo.GetRandomValues()).ToList();
        var courseStart = new DateTime(race.KickOffDate.Year, race.KickOffDate.Month, race.KickOffDate.Day, 6, 30, 0);

        return new()
        {
            RaceId = race.Id,
            SortOrder = index,
            PreferedMetric = oo.PreferedMetric,
            StartDate = courseStart,
            CourseType = oo.CourseType,
            Name = oo.CourseType.ToFriendlyText(),
            PaceType = _racePaceTypeDictionary[raceSeriesType],
            CourseInformationEntries = courseInfos
        };
    }
}
