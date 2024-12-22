namespace Api.Orchestration.Courses.GetAwardsPodium;

public record AwardWinnerDto
(
    int AthleteId,
    int AthleteCourseId,
    string FullName,
    string FinishTime,
    PaceWithTime PaceWithTime
);
