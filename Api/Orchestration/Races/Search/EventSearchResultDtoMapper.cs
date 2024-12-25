using Api.DataModels;

namespace Api.Orchestration.Races.Search;

public static class EventSearchResultDtoMapper
{
    public static List<EventSearchResultDto> GetEventSearchResultDto(List<RaceSeries> raceSeriesEntries)
    {
        return raceSeriesEntries.Select(GetEventSearchResultDto).ToList();
    }

    public static EventSearchResultDto GetEventSearchResultDto(RaceSeries raceSeries)
    {
        var upcomingRace = raceSeries.Races.OrderByDescending(oo => oo.KickOffDate).First();
        var courses = upcomingRace.Courses.Select(oo => new DisplayNameWithIdDto(oo.Id, oo.Name)).ToList();

        var eventSearchResultDto = new EventSearchResultDto
        {
            Courses = courses,
            Id = raceSeries.Id,
            LocationInfoWithRank = null,
            Name = raceSeries.Name,
            RaceSeriesTypeName = raceSeries.RaceSeriesType.ToFriendlyText(),
            UpcomingRaceId = upcomingRace.Id
        };

        return eventSearchResultDto;
    }
}
