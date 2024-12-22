namespace Api.Orchestration.Courses.GetLeaderboard;

public record CourseMetadata(List<DisplayNameWithIdDto> Courses, List<BracketMetaData> Brackets, List<DisplayNameWithIdDto> Intervals);
