using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.SearchIrps;

namespace Api.Controllers;

[Route("[controller]")]
public class SearchIrpsApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    public async Task<List<IrpSearchResultDto>> Get([FromQuery] SearchIrpsRequestDto searchRequestDto)
    {
        var orchestrator = new SearchIrpsOrchestrator(_scoringDbContext);
        return await orchestrator.GetSearchResults(searchRequestDto);
    }
}
