namespace Orchestration.GetLeaderboard.GetCourseLeaderboard;

public class CourseLeaderboardByIntervalDto
{
    public string IntervalName { get; }

    public IntervalType IntervalType { get; }

    public int TotalRacers { get; }

    public List<LeaderboardResultDto> Results { get; }

    public CourseLeaderboardByIntervalDto(string intervalName, IntervalType intervalType, int totalRacers, List<LeaderboardResultDto> results)
    {
        IntervalName = intervalName;
        IntervalType = intervalType;
        TotalRacers = totalRacers;
        Results = results;
    }
}
