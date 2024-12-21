using Api.Orchestration.SearchAllEntities;

namespace ApiTests.Orchestration.SearchAllEntities;

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
