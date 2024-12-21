using System.Threading.Tasks;
using Api.Orchestration.SearchAllEntities;
using Xunit;

namespace OrchestrationTests.SearchEvents;

public class SearchAllEntitiesOrchestratorTests
{
    [Fact]
    public async Task Test()
    {
        var dbContext = ScoringDbContextCreator.GetScoringDbContext();
        var orchestrator = new SearchAllEntitiesOrchestrator(dbContext);
        var result = await orchestrator.GetSearchResults("");

        Assert.NotNull(result);
    }
}
