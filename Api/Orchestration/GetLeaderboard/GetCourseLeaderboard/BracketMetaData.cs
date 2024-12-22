using Api.DataModels;

namespace Api.Orchestration.GetLeaderboard.GetCourseLeaderboard;

public record BracketMetaData(int Id, string DisplayName, BracketType BracketType) : DisplayNameWithIdDto(Id, DisplayName);
