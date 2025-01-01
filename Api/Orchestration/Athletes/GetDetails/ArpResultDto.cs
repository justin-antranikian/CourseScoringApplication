using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public static class ArpResultDtoMapper
{
    public static ArpResultDto GetArpResultDto(Result result, Course course, PaceWithTime paceWithTimeCumulative, MetadataGetTotalHelper metadataGetTotalHelper)
    {
        var race = course.Race;

        var overallTotal = metadataGetTotalHelper.GetOverallTotal();
        var genderTotal = metadataGetTotalHelper.GetGenderTotal();
        var primaryTotal = metadataGetTotalHelper.GetPrimaryDivisionTotal();

        return new ArpResultDto
        {
            AthleteCourseId = result.AthleteCourseId,
            CourseId = course.Id,
            CourseName = course.Name,
            GenderCount = genderTotal,
            GenderRank = result.GenderRank,
            OverallCount = overallTotal,
            OverallRank = result.OverallRank,
            PaceWithTimeCumulative = paceWithTimeCumulative,
            PrimaryDivisionCount = primaryTotal,
            PrimaryDivisionRank = result.DivisionRank,
            RaceId = course.RaceId,
            RaceName = race.Name,
        };
    }
}

public record ArpResultDto
{
    public required int AthleteCourseId { get; init; }
    public required int CourseId { get; init; }
    public required string CourseName { get; init; }
    public required int GenderCount { get; init; }
    public required int GenderRank { get; init; }
    public required int OverallCount { get; init; }
    public required int OverallRank { get; init; }
    public required PaceWithTime PaceWithTimeCumulative { get; init; }
    public required int PrimaryDivisionCount { get; init; }
    public required int PrimaryDivisionRank { get; init; }
    public required int RaceId { get; init; }
    public required string RaceName { get; init; }
}
