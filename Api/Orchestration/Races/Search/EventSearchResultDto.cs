using Api.DataModels;

namespace Api.Orchestration.Races.Search;

public static class EventSearchResultDtoMapper
{
    public static EventSearchResultDto GetEventSearchResultDto(RaceSeries raceSeries)
    {
        var upcomingRace = raceSeries.Races.OrderByDescending(oo => oo.KickOffDate).First();
        var courses = upcomingRace.Courses.Select(oo => new DisplayNameWithIdDto(oo.Id, oo.Name)).ToList();

        var eventSearchResultDto = new EventSearchResultDto
        {
            Courses = courses,
            Id = raceSeries.Id,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            Name = raceSeries.Name,
            RaceSeriesTypeName = raceSeries.RaceSeriesType.ToFriendlyText(),
            UpcomingRaceId = upcomingRace.Id
        };

        return eventSearchResultDto;
    }
}

public record EventSearchResultDto
{
    public required int Id { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string Name { get; init; }
    public required string RaceSeriesTypeName { get; init; }
    public required int UpcomingRaceId { get; init; }

    public required List<DisplayNameWithIdDto> Courses { get; init; }
}
