namespace Api.DataModels;

public enum RaceSeriesType
{
    Running,
    Triathalon,
    RoadBiking,
    MountainBiking,
    CrossCountrySkiing,
    Swim
}

public record RaceSeries
{
    public int Id { get; init; }

    public string Area { get; init; }
    public int AreaRank { get; init; }
    public string City { get; init; }
    public int CityRank { get; init; }
    public string Description { get; init; }
    public bool IsUpcoming { get; init; }
    public string Name { get; init; }
    public int OverallRank { get; init; }
    public RaceSeriesType RaceSeriesType { get; init; }
    public int Rating { get; init; }
    public string State { get; init; }
    public int StateRank { get; init; }

    public List<Race> Races { get; init; } = [];
}

public static class RaceSeriesTypeExtensions
{
    public static string ToFriendlyText(this RaceSeriesType raceSeriesType)
    {
        return raceSeriesType switch
        {
            RaceSeriesType.Running => "Running",
            RaceSeriesType.Triathalon => "Triathalon",
            RaceSeriesType.RoadBiking => "Road Biking",
            RaceSeriesType.MountainBiking => "Mountain Biking",
            RaceSeriesType.CrossCountrySkiing => "Cross Country Skiing",
            RaceSeriesType.Swim => "Swimming",
            _ => throw new NotImplementedException()
        };
    }

    public static string ToAthleteText(this RaceSeriesType raceSeriesType)
    {
        return raceSeriesType switch
        {
            RaceSeriesType.Running => "Runner",
            RaceSeriesType.Triathalon => "Triathlete",
            RaceSeriesType.RoadBiking => "Cyclist",
            RaceSeriesType.MountainBiking => "Mountain Biker",
            RaceSeriesType.CrossCountrySkiing => "Cross Country Skier",
            RaceSeriesType.Swim => "Swimmer",
            _ => throw new NotImplementedException()
        };
    }
}
