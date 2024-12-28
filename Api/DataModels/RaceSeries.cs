using Api.Orchestration;

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
    public int AreaLocationId { get; set; }
    public int CityLocationId { get; set; }
    public int StateLocationId { get; set; }

    public required int AreaRank { get; set; }
    public required int CityRank { get; set; }
    public required string Description { get; set; }
    public required string Name { get; set; }
    public required int OverallRank { get; set; }
    public required RaceSeriesType RaceSeriesType { get; set; }
    public required int StateRank { get; set; }

    public Location? AreaLocation { get; set; }
    public Location? CityLocation { get; set; }
    public Location? StateLocation { get; set; }
    public List<Race> Races { get; init; } = [];

    public LocationInfoWithRank ToLocationInfoWithRank()
    {
        return new LocationInfoWithRank
        {
            Area = AreaLocation!.Name,
            AreaRank = AreaRank,
            AreaUrl = AreaLocation.Slug,
            City = CityLocation!.Name,
            CityRank = CityRank,
            CityUrl = CityLocation.Slug,
            OverallRank = OverallRank,
            State = StateLocation!.Name,
            StateRank = StateRank,
            StateUrl = StateLocation.Slug,
        };
    }
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
