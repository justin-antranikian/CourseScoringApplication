using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public static class ArpResultDtoMapper
{
    public static ArpResultDto GetArpResultDto(ResultWithBracketType result, Course course, PaceWithTime paceWithTimeCumulative, MetadataGetTotalHelper metadataGetTotalHelper)
    {
        var race = course.Race;
        var raceSeries = race.RaceSeries;

        var overallTotal = metadataGetTotalHelper.GetOverallTotal();
        var genderTotal = metadataGetTotalHelper.GetGenderTotal();
        var primaryTotal = metadataGetTotalHelper.GetPrimaryDivisionTotal();

        var arpResultDto = new ArpResultDto
        (
            result.AthleteCourseId,
            course.RaceId,
            race.Name,
            raceSeries.RaceSeriesType,
            course.Id,
            course.Name,
            raceSeries.State,
            raceSeries.City,
            result.OverallRank,
            result.GenderRank,
            result.DivisionRank,
            overallTotal,
            genderTotal,
            primaryTotal,
            paceWithTimeCumulative
        );

        return arpResultDto;
    }
}

public record ArpResultDto
(
    int AthleteCourseId,
    int RaceId,
    string RaceName,
    RaceSeriesType RaceSeriesType,
    int CourseId,
    string CourseName,
    string State,
    string City,
    int OverallRank,
    int GenderRank,
    int PrimaryDivisionRank,
    int OverallCount,
    int GenderCount,
    int PrimaryDivisionCount,
    PaceWithTime PaceWithTimeCumulative
);
