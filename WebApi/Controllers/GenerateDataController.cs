using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GenerateData;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class GenerateDataController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<OkResult> Get()
    {
        var orchestrator = new GenerateDataOrchestrator(scoringDbContext);
        await orchestrator.Generate();
        return Ok();
    }
}
