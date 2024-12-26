namespace Api.DataModels;

public enum LocationType
{
    State,
    Area,
    City
}

public class Location
{
    public int Id { get; set; }
    public int? ParentLocationId { get; set; }

    public required LocationType LocationType { get; set; }
    public required string Name { get; set; }
    public required string Slug { get; set; }

    public List<Athlete> AreaAthletes { get; init; } = [];
    public List<RaceSeries> AreaRaceSeries { get; init; } = [];
    public List<Location> ChildLocations { get; init; } = [];
    public List<Athlete> CityAthletes { get; init; } = [];
    public List<RaceSeries> CityRaceSeries { get; init; } = [];
    public Location? ParentLocation { get; set; }
    public List<Athlete> StateAthletes { get; init; } = [];
    public List<RaceSeries> StateRaceSeries { get; init; } = [];
}