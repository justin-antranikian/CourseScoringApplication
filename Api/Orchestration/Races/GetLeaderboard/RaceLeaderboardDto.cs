using Api.DataModels;

namespace Api.Orchestration.Races.GetLeaderboard;

public static class RaceLeaderboardDtoMapper
{
    public static RaceLeaderboardDto GetRaceLeaderboardDto(Race race, List<RaceLeaderboardByCourseDto> leaderboards)
    {
        return new RaceLeaderboardDto
        {
            Leaderboards = leaderboards,
            LocationInfoWithRank = new LocationInfoWithRank(race.RaceSeries),
            RaceKickOffDate = race.KickOffDate.ToShortDateString(),
            RaceName = race.Name,
            RaceSeriesDescription = race.RaceSeries.Description,
            RaceSeriesType = race.RaceSeries.RaceSeriesType,
            RaceSeriesTypeName = race.RaceSeries.RaceSeriesType.ToString()
        };
    }
}

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
