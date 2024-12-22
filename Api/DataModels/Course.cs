using NetTopologySuite.Geometries;

namespace Api.DataModels;

public enum CourseType
{
    Running5K,
    Running10K,
    Running25K,
    HalfMarathon,
    Marathon,
    SprintTriathalon,
    OlympicTriathalon,
    HalfIronmanTriathalon,
    IronmanTriathalon,
    HalfCentryBikeRace,
    CentryBikeRace,
    MountainBikeRace,
    DownhillBikeRace,
    CrossCountrySkiing,
    FifteenHundredMeterSwim,
    OneMileSwim,
    TwoMileSwim
}

public record Course
{
    public int Id { get; init; }
    public int RaceId { get; init; }

    public CourseType CourseType { get; init; }
    public double Distance { get; set; }
    public string Name { get; init; }
    public PaceType PaceType { get; init; }
    public PreferredMetric PreferedMetric { get; init; }
    public int SortOrder { get; init; }
    public DateTime StartDate { get; init; }
    public Geometry? Location { get; set; }

    public List<AthleteCourse> AthleteCourses { get; set; } = [];
    public List<AthleteCourseBracket> AthleteCourseBrackets { get; init; } = [];
    public List<Bracket> Brackets { get; init; }
    public List<BracketMetadata> BracketMetadatas { get; init; } = [];
    public List<CourseInformationEntry> CourseInformationEntries { get; init; } = [];
    public List<Interval> Intervals { get; init; }
    public Race Race { get; init; }
    public List<Result> Results { get; set; } = [];
    public List<TagRead> TagReads { get; set; } = [];
}

public static class CourseTypeExtensions
{
    public static string ToFriendlyText(this CourseType courseType)
    {
        return courseType switch
        {
            CourseType.Running5K => "5 K",
            CourseType.Running10K => "10 K",
            CourseType.Running25K => "25 K",
            CourseType.HalfMarathon => "Half Marathon",
            CourseType.Marathon => "Marathon",
            CourseType.SprintTriathalon => "Sprint",
            CourseType.OlympicTriathalon => "Olympic",
            CourseType.HalfIronmanTriathalon => "Half Ironman",
            CourseType.IronmanTriathalon => "Ironman",
            CourseType.HalfCentryBikeRace => "Half Century",
            CourseType.CentryBikeRace => "Century",
            CourseType.MountainBikeRace => "Mountain Bike",
            CourseType.DownhillBikeRace => "Downhill Bike",
            CourseType.CrossCountrySkiing => "10 K",
            CourseType.FifteenHundredMeterSwim => "1500 Meters",
            CourseType.OneMileSwim => "1 Mile",
            CourseType.TwoMileSwim => "2 Miles",
            _ => throw new NotImplementedException()
        };
    }
}
