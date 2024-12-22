using Api.DataModels;
using Api.Orchestration.Races.GetLeaderboard;

namespace ApiTests.Orchestration.GetLeaderboard.GetRaceLeaderboard;

public class RaceLeaderboardDtoTests
{
    [Fact]
    public void MapsAllFields()
    {
        var race = new Race
        {
            Name = "RA",
            KickOffDate = new DateTime(2010,
                1,
                1,
                6,
                30,
                0),
            RaceSeries = new()
            {
                Description = "DA",
                RaceSeriesType = RaceSeriesType.RoadBiking,
                Area = null,
                AreaRank = 0,
                City = null,
                CityRank = 0,
                Name = null,
                OverallRank = 0,
                Rating = 0,
                State = null,
                StateRank = 0
            },
            TimeZoneId = ""
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
