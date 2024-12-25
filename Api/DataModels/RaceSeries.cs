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

public class RaceSeries
{
    public int Id { get; set; }

    public required string Area { get; set; }
    public required int AreaRank { get; set; }
    public required string City { get; set; }
    public required int CityRank { get; set; }
    public required string Description { get; set; }
    public required string Name { get; set; }
    public required int OverallRank { get; set; }
    public required RaceSeriesType RaceSeriesType { get; set; }
    public required int Rating { get; set; }
    public required string State { get; set; }
    public required int StateRank { get; set; }

    public List<Race> Races { get; set; } = [];
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
