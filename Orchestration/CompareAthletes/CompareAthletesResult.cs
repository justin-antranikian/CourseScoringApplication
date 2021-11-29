using Core;

namespace Orchestration.CompareAthletes
{
	public record CompareAthletesResult
	(
		int AthleteCourseId,
		int RaceId,
		string RaceName,
		int CourseId,
		string CourseName,
		PaceWithTime PaceWithTime,
		int OverallRank,
		int GenderRank,
		int DivisionRank
	);
}
