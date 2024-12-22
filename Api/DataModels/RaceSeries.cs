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

    public required string Area { get; init; }
    public required int AreaRank { get; init; }
    public required string City { get; init; }
    public required int CityRank { get; init; }
    public required string Description { get; init; }
    public required string Name { get; init; }
    public required int OverallRank { get; init; }
    public required RaceSeriesType RaceSeriesType { get; init; }
    public required int Rating { get; init; }
    public required string State { get; init; }
    public required int StateRank { get; init; }

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
