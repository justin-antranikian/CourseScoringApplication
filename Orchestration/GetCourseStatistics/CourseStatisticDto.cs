namespace Orchestration.GetCourseStatistics;

public record CourseStatisticDto
(
    int? BracketId,
    PaceWithTime AveragePaceWithTime,
    PaceWithTime FastestPaceWithTime,
    PaceWithTime SlowestPaceWithTime
);
