using Core;
using System.Collections.Generic;

namespace DataModels;

public record RaceSeries
{
    public int Id { get; init; }

    public string Name { get; init; }
    public string Description { get; init; }
    public RaceSeriesType RaceSeriesType { get; init; }
    public string State { get; init; }
    public string Area { get; init; }
    public string City { get; init; }
    public int OverallRank { get; init; }
    public int StateRank { get; init; }
    public int AreaRank { get; init; }
    public int CityRank { get; init; }
    public bool IsUpcoming { get; init; }
    public int Rating { get; init; }

    public List<Race> Races { get; init; } = [];
}
