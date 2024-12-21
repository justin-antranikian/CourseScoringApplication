using Api.DataModels;
using Api.Orchestration.SearchAllEntities;
using Microsoft.AspNetCore.Mvc;


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
