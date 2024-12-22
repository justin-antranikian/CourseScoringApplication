using Api.DataModels;

namespace Api.Orchestration.Races.GetLeaderboard;

public record RaceLeaderboardDto
{
    public required List<RaceLeaderboardByCourseDto> Leaderboards { get; init; }
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string RaceKickOffDate { get; init; }
    public required string RaceName { get; init; }
    public required string RaceSeriesDescription { get; init; }
    public required RaceSeriesType RaceSeriesType { get; init; }
    public required string RaceSeriesTypeName { get; init; }
}
