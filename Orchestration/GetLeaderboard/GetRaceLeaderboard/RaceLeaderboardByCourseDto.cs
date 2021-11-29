using Core;
using System.Collections.Generic;

namespace Orchestration.GetLeaderboard.GetRaceLeaderboard
{
	public record RaceLeaderboardByCourseDto
	(
		int CourseId,
		string CourseName,
		int SortOrder,
		string HighestIntervalName,
		IntervalType IntervalType,
		List<LeaderboardResultDto> Results
	);
}
