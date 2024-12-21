namespace Api.Orchestration.GetLeaderboard;

public class LeaderboardResultDto
{
    public required int AthleteCourseId { get; init; }
    public required int AthleteId { get; init; }
    public required string Bib { get; init; }
    public required int DivisionRank { get; init; }
    public required int GenderRank { get; init; }
    public required string GenderAbbreviated { get; init; }
    public required string FullName { get; init; }
    public required int OverallRank { get; init; }
    public required int RaceAge { get; init; }
    public required PaceWithTime PaceWithTimeCumulative { get; init; }
}
