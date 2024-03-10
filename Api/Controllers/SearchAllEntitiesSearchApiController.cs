using Microsoft.AspNetCore.Mvc;
using DataModels;
using Orchestration.GetSearchAllEntitiesSearch;

namespace Api.Controllers;

[Route("[controller]")]
public class SearchAllEntitiesSearchApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    public async Task<AllEntitiesSearchResultDto> Get([FromQuery] string searchTerm)
    {
        var orchestrator = new SearchAllEntitiesOrchestrator(_scoringDbContext);
        return await orchestrator.GetSearchResults(searchTerm);
    }
}
