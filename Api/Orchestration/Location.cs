namespace Api.Orchestration;

public class Location
{
    public readonly string State;
    public readonly string StateUrlFriendly;
    public readonly string StateDescription;
    public readonly string Area;
    public readonly string AreaUrlFriendly;
    public readonly string AreaDescription;
    public readonly string City;
    public readonly string CityUrlFriendly;
    public readonly string CityDescription;

    public Location(string state, string area, string city)
    {
        State = state;
        StateUrlFriendly = state.ToUrlFriendlyText();
        StateDescription = GetDescription(state);
        Area = area;
        AreaUrlFriendly = area.ToUrlFriendlyText();
        AreaDescription = GetDescription(area);
        City = city;
        CityUrlFriendly = city.ToUrlFriendlyText();
        CityDescription = GetDescription(city);
    }

    private static string GetDescription(string displayName)
    {
        var possibleValues = new[]
        {
            $"Situated at the base of the iconic mountains, {displayName} is known for its outdoor activities and abundant sunshine.",
            $"{displayName} has a lot going for it in terms of events.",
            $"{displayName} is home to many top competetor and events. There is never a dull moment here.",
            $"Make sure to compete in one of the events here. It should be on your bucket list.",
        };

        var distintValues = possibleValues.GetRandomValues().Distinct().ToArray();
        var description = string.Join(' ', distintValues);
        return description;
    }
}
