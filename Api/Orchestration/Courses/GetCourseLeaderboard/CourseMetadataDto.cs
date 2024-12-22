namespace Api.Orchestration.Courses.GetCourseLeaderboard;

public record CourseMetadata(List<DisplayNameWithIdDto> Courses, List<BracketMetaData> Brackets, List<DisplayNameWithIdDto> Intervals);
