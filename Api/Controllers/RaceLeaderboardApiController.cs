using Api.DataModels;
using Api.Orchestration.Races.GetRaceLeaderboard;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class RaceLeaderboardApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{raceId:int}")]
    public async Task<RaceLeaderboardDto> Get(int raceId)
    {
        var orchestrator = new GetRaceLeaderboardOrchestrator(scoringDbContext);
        return await orchestrator.GetRaceLeaderboardDto(raceId);
    }
}
