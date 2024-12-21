namespace Api.DataModels.Enums;

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
