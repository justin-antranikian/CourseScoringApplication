namespace Api.Orchestration;

public class LocationInfoWithUrl
{
    public required string Area { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }

    public string StateUrl => State.ToUrlFriendlyText();
    public string AreaUrl => Area.ToUrlFriendlyText();
    public string CityUrl => City.ToUrlFriendlyText();
}
