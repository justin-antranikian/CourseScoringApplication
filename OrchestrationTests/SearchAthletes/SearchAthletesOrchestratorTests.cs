using Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataModels;
using Api.Orchestration.SearchAthletes;
using Xunit;

namespace OrchestrationTests.SearchEvents;

public class SearchAthletesOrchestratorTests
{
    [Fact]
    public async Task NoFilters()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchAthletesRequestDto();
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 3, 2, 4, 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnState()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchAthletesRequestDto { State = "colorado" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 3, 2, 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnArea()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchAthletesRequestDto { Area = "greater-denver-area" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 2, 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnCity()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchAthletesRequestDto { City = "denver" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    [Fact]
    public async Task FilterOnFullName()
    {
        var orchestrator = GetOrchestrator();
        var request = new SearchAthletesRequestDto { SearchTerm = "AA" };
        var searchResults = await orchestrator.GetSearchResults(request);

        Assert.Equal(new[] { 2, 1 }, searchResults.Select(oo => oo.Id).ToArray());
    }

    #region test preperation methods

    private static SearchAthletesOrchestrator GetOrchestrator()
    {
        var dbContext = ScoringDbContextCreator.GetEmptyDbContext();
        dbContext.Athletes.AddRange(GetAthletes());
        dbContext.SaveChangesAsync();
        return new SearchAthletesOrchestrator(dbContext);
    }

    private static List<Athlete> GetAthletes()
    {
        var athletes = new[]
        {
            new Athlete
            {
                Id = 1,
                FullName = "AAA",
                Gender = Gender.Male,
                State = "Colorado",
                Area = "Greater Denver Area",
                City = "Denver",
                OverallRank = 4,
                AthleteRaceSeriesGoals = new List<AthleteRaceSeriesGoal>()
            },
            new Athlete
            {
                Id = 2,
                FullName = "AAB",
                Gender = Gender.Male,
                State = "Colorado",
                Area = "Greater Denver Area",
                City = "Boulder",
                OverallRank = 2,
                AthleteRaceSeriesGoals = new List<AthleteRaceSeriesGoal>()
            },
            new Athlete
            {
                Id = 3,
                FullName = "BBB",
                Gender = Gender.Male,
                State = "Colorado",
                Area = "Greater Colorado Springs Area",
                City = "Colorado Springs",
                OverallRank = 1,
                AthleteRaceSeriesGoals = new List<AthleteRaceSeriesGoal>()
            },
            new Athlete
            {
                Id = 4,
                FullName = "CCC",
                Gender = Gender.Male,
                State = "California",
                Area = "Greater San Diego Area",
                City = "San Diego",
                OverallRank = 3,
                AthleteRaceSeriesGoals = new List<AthleteRaceSeriesGoal>()
            }
        };

        return athletes.ToList();
    }

    #endregion
}
