using Api.DataModels;
using System.Diagnostics;

namespace Api.Orchestration.Races.Search;

public static class EventSearchResultDtoMapper
{
    public static EventSearchResultDto GetEventSearchResultDto(RaceSeries raceSeries)
    {
        var upcomingRace = raceSeries.Races.OrderByDescending(oo => oo.KickOffDate).First();
        var courses = upcomingRace.Courses.Select(oo => new DisplayNameWithIdDto(oo.Id, oo.Name)).ToList();

        return new EventSearchResultDto
        {
            Courses = courses,
            Id = raceSeries.Id,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            Name = raceSeries.Name,
            RaceKickOffDate = upcomingRace.KickOffDate.ToShortDateString(),
            RaceSeriesTypeName = raceSeries.RaceSeriesType.ToFriendlyText(),
            UpcomingRaceId = upcomingRace.Id
        };
    }
}

public record EventSearchResultDto
{
    public required int Id { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string Name { get; init; }
    public required string RaceKickOffDate { get; init; }
    public required string RaceSeriesTypeName { get; init; }
    public required int UpcomingRaceId { get; init; }

    public required List<DisplayNameWithIdDto> Courses { get; init; }
}
