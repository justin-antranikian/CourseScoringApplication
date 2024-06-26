﻿using Orchestration.GetSearchAllEntitiesSearch;
using System.Threading.Tasks;
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
