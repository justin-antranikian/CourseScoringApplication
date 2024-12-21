using Api.DataModels;
using Api.DataModels.Enums;

namespace Api.Orchestration.GetLeaderboard;

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
