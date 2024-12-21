using Api.DataModels.Enums;
using Api.Orchestration.GetRaceSeriesDashboard;

namespace ApiTests.Orchestration.GetRaceSeriesDashboard;

public class GetRaceSeriesDashboardOrchestratorTests
{
    [Fact]
    public async Task MapsAllFields()
    {
        var dbContext = ScoringDbContextCreator.GetScoringDbContext();
        var orchestrator = new GetRaceSeriesDashboardOrchestrator(dbContext);

        var raceSeriesDto = await orchestrator.GetRaceSeriesDashboardDto(1);

        Assert.Equal("Houston Triathalons", raceSeriesDto.Name);
        Assert.Equal("All Houston Triathalons", raceSeriesDto.Description);
        Assert.Equal("1/1/2011", raceSeriesDto.KickOffDate);

        Assert.Collection(raceSeriesDto.Races, result =>
        {
            Assert.Equal(2, result.Id);
            Assert.Equal("2011 Houston Triathalon", result.DisplayName);
            Assert.Equal("1/1/2011", result.KickOffDate);
        }, result =>
        {
            Assert.Equal(1, result.Id);
            Assert.Equal("2010 Houston Triathalon", result.DisplayName);
            Assert.Equal("1/1/2010", result.KickOffDate);
        });

        Assert.Collection(raceSeriesDto.Courses, result =>
        {
            Assert.Equal(3, result.Id);
            Assert.Equal("Course 3", result.DisplayName);
            Assert.Empty(result.DescriptionEntries);
            Assert.Empty(result.PromotionalEntries);
            Assert.Empty(result.HowToPrepareEntries);

            var participantIds = result.Participants.Select(oo => oo.AthleteId).ToArray();
            Assert.Equal(new[] { 1, 2, 3, 4 }, participantIds);
        });

        Assert.Equal(RaceSeriesType.Triathalon, raceSeriesDto.RaceSeriesType);

        var locationInfo = raceSeriesDto.LocationInfoWithRank;
        Assert.Equal(4, locationInfo.OverallRank);
        Assert.Equal("Colorado", locationInfo.State);
        Assert.Equal("colorado", locationInfo.StateUrl);
        Assert.Equal(3, locationInfo.StateRank);
        Assert.Equal("Area 1", locationInfo.Area);
        Assert.Equal("area-1", locationInfo.AreaUrl);
        Assert.Equal(2, locationInfo.AreaRank);
        Assert.Equal("City 1", locationInfo.City);
        Assert.Equal("city-1", locationInfo.CityUrl);
        Assert.Equal(1, locationInfo.CityRank);

        Assert.Equal(2, raceSeriesDto.UpcomingRaceId);
        Assert.Equal(3, raceSeriesDto.FirstCourseId);
    }
}
