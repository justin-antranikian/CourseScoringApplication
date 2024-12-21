using Api.DataModels;

namespace Api.Orchestration.GetLeaderboard.GetRaceLeaderboard;

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
