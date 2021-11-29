using Core;

namespace Orchestration.GetLeaderboard.GetCourseLeaderboard
{
	public record BracketMetaData : DisplayNameWithIdDto
	{
		public BracketType BracketType { get; }

		public BracketMetaData(int id, string displayName, BracketType bracketType) : base(id, displayName)
		{
			BracketType = bracketType;
		}
	}
}
