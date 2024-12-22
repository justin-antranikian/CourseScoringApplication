using Api.DataModels;
using Api.Orchestration.Results.Search;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class SearchIrpsApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<List<IrpSearchResultDto>> Get([FromQuery] SearchIrpsRequestDto searchRequestDto)
    {
        var orchestrator = new SearchIrpsOrchestrator(scoringDbContext);
        return await orchestrator.GetSearchResults(searchRequestDto);
    }
}
