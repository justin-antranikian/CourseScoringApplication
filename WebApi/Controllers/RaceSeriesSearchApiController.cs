using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetRaceSeriesSearch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class RaceSeriesSearchApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    public async Task<List<EventSearchResultDto>> Get([FromQuery] SearchEventsRequestDto requestDto)
    {
        var orchestrator = new SearchEventsOrchestrator(_scoringDbContext);
        return await orchestrator.GetSearchResults(requestDto);
    }
}
