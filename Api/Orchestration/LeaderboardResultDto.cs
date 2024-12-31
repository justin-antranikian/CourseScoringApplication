using Api.DataModels;

namespace Api.Orchestration;
public static class LeaderboardResultDtoMapper
{
    public static LeaderboardResultDto GetLeaderboardResultDto(Result result, Athlete athlete, PaceWithTime paceWithTimeCumulative, Course course)
    {
        return new LeaderboardResultDto
        {
            AthleteCourseId = result.AthleteCourseId,
            AthleteId = athlete.Id,
            Bib = result.AthleteCourse.Bib,
            DivisionRank = result.DivisionRank,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            GenderRank = result.GenderRank,
            OverallRank = result.OverallRank,
            PaceWithTimeCumulative = paceWithTimeCumulative,
            RaceAge = athlete.GetRaceAge(course.StartDate)
        };
    }
}

public class LeaderboardResultDto
{
    public required int AthleteCourseId { get; init; }
    public required int AthleteId { get; init; }
    public required string Bib { get; init; }
    public required int DivisionRank { get; init; }
    public required int GenderRank { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required string FullName { get; init; }
    public required int OverallRank { get; init; }
    public required int RaceAge { get; init; }
    public required PaceWithTime PaceWithTimeCumulative { get; init; }
}
