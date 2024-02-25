using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetArp;

namespace WebApplicationSandbox.Controllers;

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
