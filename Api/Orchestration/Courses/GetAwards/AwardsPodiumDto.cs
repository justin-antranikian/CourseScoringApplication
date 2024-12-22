namespace Api.Orchestration.Courses.GetAwards;

public record AwardWinnerDto
(
    int AthleteId,
    int AthleteCourseId,
    string FullName,
    string FinishTime,
    PaceWithTime PaceWithTime
);
