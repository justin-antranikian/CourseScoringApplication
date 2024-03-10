using Microsoft.AspNetCore.Mvc;
using DataModels;
using Orchestration.GetArp;

namespace Api.Controllers;

[Route("[controller]")]
public class ArpApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{athleteId:int}")]
    public async Task<ArpDto> Get(int athleteId)
    {
        var orchestrator = new GetArpOrchestrator(scoringDbContext);
        return await orchestrator.GetArpDto(athleteId);
    }
}
