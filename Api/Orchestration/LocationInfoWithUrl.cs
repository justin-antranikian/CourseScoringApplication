using Api.DataModels;
using Core;

namespace Api.Orchestration;

public class LocationInfoWithUrl(string state, string area, string city)
{
    public string State { get; } = state;
    public string StateUrl { get => State.ToUrlFriendlyText(); }
    public string Area { get; } = area;
    public string AreaUrl { get => Area.ToUrlFriendlyText(); }
    public string City { get; } = city;
    public string CityUrl { get => City.ToUrlFriendlyText(); }

    public LocationInfoWithUrl(Location location) : this(location.State, location.Area, location.City) { }

    public LocationInfoWithUrl(RaceSeries raceSeries) : this(raceSeries.State, raceSeries.Area, raceSeries.City) { }

    public LocationInfoWithUrl(Athlete athlete) : this(athlete.State, athlete.Area, athlete.City) { }
}
