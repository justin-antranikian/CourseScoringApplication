using Microsoft.AspNetCore.Mvc;
using DataModels;
using Orchestration.GetSearchAllEntitiesSearch;

namespace Api.Controllers;

[Route("[controller]")]
public class SearchAllEntitiesSearchApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<AllEntitiesSearchResultDto> Get([FromQuery] string searchTerm)
    {
        var orchestrator = new SearchAllEntitiesOrchestrator(scoringDbContext);
        return await orchestrator.GetSearchResults(searchTerm);
    }
}
