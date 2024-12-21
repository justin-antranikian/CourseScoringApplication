using Api.DataModels;
using Api.Orchestration.GetRaceSeriesDashboard;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class RaceSeriesDashboardApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{raceSeriesId:int}")]
    public async Task<RaceSeriesDashboardDto> Get(int raceSeriesId)
    {
        var orchestrator = new GetRaceSeriesDashboardOrchestrator(scoringDbContext);
        return await orchestrator.GetRaceSeriesDashboardDto(raceSeriesId);
    }
}
