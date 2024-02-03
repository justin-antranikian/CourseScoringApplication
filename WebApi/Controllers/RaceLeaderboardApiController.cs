using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetLeaderboard.GetRaceLeaderboard;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class RaceLeaderboardApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    [Route("{raceId:int}")]
    public async Task<RaceLeaderboardDto> Get(int raceId)
    {
        var orchestrator = new GetRaceLeaderboardOrchestrator(_scoringDbContext);
        return await orchestrator.GetRaceLeaderboardDto(raceId);
    }
}
