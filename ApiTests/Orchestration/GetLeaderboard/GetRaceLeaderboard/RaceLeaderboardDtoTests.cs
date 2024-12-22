using Api.DataModels;
using Api.Orchestration.GetLeaderboard.GetRaceLeaderboard;

namespace ApiTests.Orchestration.GetLeaderboard.GetRaceLeaderboard;

public class RaceLeaderboardDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var race = new Race
        {
            Name = "RA",
            KickOffDate = new DateTime(2010, 1, 1, 6, 30, 0),
            RaceSeries = new()
            {
                Description = "DA",
                RaceSeriesType = RaceSeriesType.RoadBiking
            }
        };

        var raceLeaderboardDto = RaceLeaderboardDtoMapper.GetRaceLeaderboardDto(race, new());

        Assert.Equal("RA", raceLeaderboardDto.RaceName);
        Assert.Equal("DA", raceLeaderboardDto.RaceSeriesDescription);
        Assert.Equal("1/1/2010", raceLeaderboardDto.RaceKickOffDate);
        Assert.Equal(RaceSeriesType.RoadBiking, raceLeaderboardDto.RaceSeriesType);
        Assert.NotNull(raceLeaderboardDto.LocationInfoWithRank);
        Assert.Empty(raceLeaderboardDto.Leaderboards);
    }
}
