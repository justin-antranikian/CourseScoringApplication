namespace Orchestration.GetLeaderboard;

public static class LeaderboardResultDtoMapper
{
	public static LeaderboardResultDto GetLeaderboardResultDto(Result result, Athlete athlete, PaceWithTime paceWithTimeCumulative, Course course)
	{
		return new
		(
			athlete.Id,
			athlete.FullName,
			athlete.DateOfBirth,
			athlete.Gender,
			result.AthleteCourse.Bib,
			course.StartDate,
			paceWithTimeCumulative,
			result.AthleteCourseId,
			result.OverallRank,
			result.GenderRank,
			result.DivisionRank
		);
	}
}

public class LeaderboardResultDto : AthleteResultBase
{
	public int AthleteCourseId { get; }

	public int OverallRank { get; }

	public int GenderRank { get; }

	public int DivisionRank { get; }

	public LeaderboardResultDto
	(
		int athleteId,
		string fullName,
		DateTime dateOfBirth,
		Gender gender,
		string bib,
		DateTime courseStartTime,
		PaceWithTime paceWithTimeCumulative,
		int athleteCourseId,
		int overallRank,
		int genderRank,
		int divisionRank
	) : base
	(
		athleteId,
		fullName,
		dateOfBirth,
		gender,
		bib,
		courseStartTime,
		paceWithTimeCumulative
	)
	{
		AthleteCourseId = athleteCourseId;
		OverallRank = overallRank;
		GenderRank = genderRank;
		DivisionRank = divisionRank;
	}
}
