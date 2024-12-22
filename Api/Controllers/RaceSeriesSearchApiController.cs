using Api.DataModels;
using Api.Orchestration.Races.Search;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class RaceSeriesSearchApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<List<EventSearchResultDto>> Get([FromQuery] SearchEventsRequestDto requestDto)
    {
        var orchestrator = new SearchEventsOrchestrator(scoringDbContext);
        return await orchestrator.GetSearchResults(requestDto);
    }
}
