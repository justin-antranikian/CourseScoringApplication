﻿namespace Api.Orchestration.Races.GetLeaderboard;

public record RaceLeaderboardDto
{
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string RaceKickOffDate { get; init; }
    public required string RaceName { get; init; }
    public required string RaceSeriesType { get; init; }

    public required List<RaceLeaderboardByCourseDto> Leaderboards { get; init; }
}
