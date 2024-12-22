using Api.DataModels;

namespace Api.Orchestration.Courses.GetLeaderboard;

public record BracketMetaData(int Id, string DisplayName, BracketType BracketType) : DisplayNameWithIdDto(Id, DisplayName);
