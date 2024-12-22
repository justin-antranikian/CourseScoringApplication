using Api.DataModels;
using Api.Orchestration.Athletes.GetDetails;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

[Route("[controller]")]
public class ArpApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{athleteId:int}")]
    public async Task<ArpDto> Get(int athleteId)
    {
        var orchestrator = new GetAthleteDetailsOrchestrator(scoringDbContext);
        return await orchestrator.GetArpDto(athleteId);
    }
}
