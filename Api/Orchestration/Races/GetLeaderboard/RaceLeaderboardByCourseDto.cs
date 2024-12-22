using Api.DataModels;

namespace Api.Orchestration.Races.GetLeaderboard;

public record RaceLeaderboardByCourseDto
(
    int CourseId,
    string CourseName,
    int SortOrder,
    string HighestIntervalName,
    IntervalType IntervalType,
    List<LeaderboardResultDto> Results
);
