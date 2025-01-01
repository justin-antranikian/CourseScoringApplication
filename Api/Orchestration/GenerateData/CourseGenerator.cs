using Api.DataModels;
using Bogus;

namespace Api.Orchestration.GenerateData;

public static class CourseGenerator
{
    private static readonly Dictionary<RaceSeriesType, CourseType[]> _raceAndCourseTypeDictionary = new()
    {
        { RaceSeriesType.Triathalon, [CourseType.SprintTriathalon, CourseType.OlympicTriathalon, CourseType.HalfIronmanTriathalon, CourseType.IronmanTriathalon] },
        { RaceSeriesType.Running, [CourseType.Running5K, CourseType.Running10K, CourseType.Running25K, CourseType.HalfMarathon, CourseType.Marathon] },
        { RaceSeriesType.RoadBiking, [CourseType.HalfCentryBikeRace, CourseType.CentryBikeRace] },
        { RaceSeriesType.MountainBiking, [CourseType.MountainBikeRace, CourseType.DownhillBikeRace] },
        { RaceSeriesType.CrossCountrySkiing, [CourseType.CrossCountrySkiing] },
        { RaceSeriesType.Swim, [CourseType.FifteenHundredMeterSwim, CourseType.OneMileSwim, CourseType.TwoMileSwim] },
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
                .RuleFor(oo => oo.PreferedMetric, _ => typeof(PreferredMetric).GetRandomEnumValue())
                .RuleFor(oo => oo.CourseType, _ => possibleCourseTypes.GetRandomEnumValue()
            );

            var coursesForRace = courseFaker.Generate(3).Select((oo, index) => GetCourse(oo, index, race));
            courses.AddRange(coursesForRace);
        }

        return courses;
    }

    private static Course GetCourse(Course oo, int index, Race race)
    {
        var raceSeriesType = race.RaceSeries.RaceSeriesType;
        var courseStart = new DateTime(race.KickOffDate.Year, race.KickOffDate.Month, race.KickOffDate.Day, 6, 30, 0);

        return new()
        {
            RaceId = race.Id,
            CourseType = oo.CourseType,
            Distance = 0,
            Name = oo.CourseType.ToFriendlyText(),
            PaceType = _racePaceTypeDictionary[raceSeriesType],
            PreferedMetric = oo.PreferedMetric,
            SortOrder = index,
            StartDate = courseStart,
        };
    }
}
