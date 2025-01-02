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
