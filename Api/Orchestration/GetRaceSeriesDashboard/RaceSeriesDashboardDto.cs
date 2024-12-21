using Api.DataModels;
using Api.DataModels.Enums;

namespace Api.Orchestration.GetRaceSeriesDashboard;

public static class RaceSeriesDashboardDtoMapper
{
    public static RaceSeriesDashboardDto GetRaceSeriesDashboardDto(RaceSeries raceSeries, List<PastRaceDto> races, List<RaceSeriesDashboardCourseDto> courses)
    {
        var upcomingRace = races.First();
        var firstCourse = courses.First();

        return new
        (
            raceSeries.Name,
            raceSeries.Description,
            upcomingRace.KickOffDate,
            races,
            courses,
            raceSeries.RaceSeriesType,
            new LocationInfoWithRank(raceSeries),
            upcomingRace.Id,
            firstCourse.Id
        );
    }
}

public record RaceSeriesDashboardDto
(
    string Name,
    string Description,
    string KickOffDate,
    List<PastRaceDto> Races,
    List<RaceSeriesDashboardCourseDto> Courses,
    RaceSeriesType RaceSeriesType,
    LocationInfoWithRank LocationInfoWithRank,
    int UpcomingRaceId,
    int FirstCourseId
);

