using Microsoft.AspNetCore.Mvc;
using DataModels;
using Orchestration.GetLeaderboard.GetRaceLeaderboard;

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
