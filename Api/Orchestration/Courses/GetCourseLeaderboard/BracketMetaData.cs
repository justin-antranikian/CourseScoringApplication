using Api.DataModels;

namespace Api.Orchestration.Courses.GetCourseLeaderboard;

public record BracketMetaData(int Id, string DisplayName, BracketType BracketType) : DisplayNameWithIdDto(Id, DisplayName);
