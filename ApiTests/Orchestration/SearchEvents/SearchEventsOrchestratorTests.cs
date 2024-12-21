using Api.DataModels;
using Api.DataModels.Enums;
using Api.Orchestration.SearchEvents;

namespace ApiTests.Orchestration.SearchEvents;

public class SearchEventsOrchestratorTests
{
    [Fact]
    public async Task NoFilters()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchEventsRequestDto();
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 2, 4, 1, 3 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnState()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchEventsRequestDto { State = "colorado" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 2, 1, 3 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnArea()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchEventsRequestDto { Area = "greater-denver-area" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 2, 1, }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnCity()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchEventsRequestDto { City = "denver" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnName()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchEventsRequestDto { SearchTerm = "AA" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 2, 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnRaceSeriesTypes()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchEventsRequestDto(RaceSeriesType.Running, RaceSeriesType.Swim);
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 2, 4, 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    #region test preperation methods

    public static SearchEventsOrchestrator GetOrchestrator()
    {
        var dbContext = ScoringDbContextCreator.GetEmptyDbContext();

        static List<Race> GetRaces()
        {
            return new List<Race>
            {
                new()
                {
                    Name = "RAAA",
                    Courses = new()
                    {
                        new() { Name = "TAAA" }
                    }
                }
            };
        }

        var raceSeriesEntries = new List<RaceSeries>
        {
            new()
            {
                Id = 1,
                Name = "AAA",
                State = "Colorado",
                Area = "Greater Denver Area",
                City = "Denver",
                OverallRank = 3,
                IsUpcoming = true,
                RaceSeriesType = RaceSeriesType.Running,
                Races = GetRaces(),
            },
            new()
            {
                Id = 2,
                Name = "AAA",
                State = "Colorado",
                Area = "Greater Denver Area",
                City = "Boulder",
                OverallRank = 1,
                RaceSeriesType = RaceSeriesType.Running,
                Races = GetRaces(),
            },
            new()
            {
                Id = 3,
                Name = "BBB",
                State = "Colorado",
                Area = "Greater Colorado Springs Area",
                City = "Colorado Springs",
                OverallRank = 4,
                RaceSeriesType = RaceSeriesType.RoadBiking,
                Races = GetRaces(),
            },
            new()
            {
                Id = 4,
                Name = "CCC",
                State = "California",
                Area = "Greater San Diego Area",
                City = "San Diego",
                OverallRank = 2,
                RaceSeriesType = RaceSeriesType.Swim,
                Races = GetRaces(),
            },
        };

        dbContext.RaceSeries.AddRange(raceSeriesEntries);
        dbContext.SaveChangesAsync();
        return new SearchEventsOrchestrator(dbContext);
    }

    #endregion
}
