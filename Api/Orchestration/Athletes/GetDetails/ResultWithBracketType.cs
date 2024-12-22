using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public record ResultWithBracketType
(
    int AthleteCourseId,
    int BracketId,
    BracketType BracketType,
    int CourseId,
    int IntervalId,
    int TimeOnCourse,
    int OverallRank,
    int GenderRank,
    int DivisionRank
);
