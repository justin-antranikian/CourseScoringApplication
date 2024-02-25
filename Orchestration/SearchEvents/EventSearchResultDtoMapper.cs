using Orchestration.GetRaceSeriesSearch;

namespace Orchestration.SearchEvents;

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
        {
            Courses = courses,
            Description = raceSeries.Description,
            Id = raceSeries.Id,
            KickOffDate = dateFormatted,
            KickOffTime = timeFormatted,
            LocationInfoWithRank = new LocationInfoWithRank(raceSeries),
            Name = raceSeries.Name,
            RaceSeriesType = raceSeries.RaceSeriesType,
            RaceSeriesTypeName = raceSeries.RaceSeriesType.ToFriendlyText(),
            Rating = raceSeries.Rating,
            UpcomingRaceId = upcomingRace.Id
        };

        return eventSearchResultDto;
    }
}
