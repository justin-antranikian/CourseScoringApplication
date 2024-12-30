using Api.DataModels;

namespace Api.Orchestration.Races.GetLeaderboard;

public static class RaceLeaderboardDtoMapper
{
    public static RaceLeaderboardDto GetRaceLeaderboardDto(Race race, List<RaceLeaderboardByCourseDto> leaderboards)
    {
        var raceSeries = race.RaceSeries;

        return new RaceLeaderboardDto
        {
            Leaderboards = leaderboards,
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            RaceKickOffDate = race.KickOffDate.ToShortDateString(),
            RaceName = race.Name,
            RaceSeriesDescription = raceSeries.Description,
            RaceSeriesType = raceSeries.RaceSeriesType.ToString(),
        };
    }
}

public record RaceLeaderboardDto
{
    public required LocationInfoWithRank LocationInfoWithRank { get; init; }
    public required string RaceKickOffDate { get; init; }
    public required string RaceName { get; init; }
    public required string RaceSeriesDescription { get; init; }
    public required string RaceSeriesType { get; init; }

    public required List<RaceLeaderboardByCourseDto> Leaderboards { get; init; }
}
