namespace Orchestration.GetRaceSeriesSearch;

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
        var (dateFormatted, timeFormatted) = DateTimeHelper.GetFormattedFields(upcomingRace.KickOffDate);

        var eventSearchResultDto = new EventSearchResultDto
        (
            raceSeries.Id,
            raceSeries.Name,
            raceSeries.RaceSeriesType,
            raceSeries.RaceSeriesType.ToFriendlyText(),
            upcomingRace.Id,
            dateFormatted,
            timeFormatted,
            raceSeries.Description,
            courses,
            new LocationInfoWithRank(raceSeries),
            raceSeries.Rating
        );

        return eventSearchResultDto;
    }
}

public record EventSearchResultDto
(
    int Id,
    string Name,
    RaceSeriesType RaceSeriesType,
    string RaceSeriesTypeName,
    int UpcomingRaceId,
    string KickOffDate,
    string KickOffTime,
    string Description,
    List<DisplayNameWithIdDto> Courses,
    LocationInfoWithRank LocationInfoWithRank,
    int Rating
);
