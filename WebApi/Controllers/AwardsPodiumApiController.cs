using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetAwardsPodium;
using System.Collections.Generic;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class AwardsPodiumApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{courseId:int}")]
    public async Task<List<PodiumEntryDto>> Get(int courseId)
    {
        var orchestrator = new GetAwardsPodiumOrchestrator(scoringDbContext);
        return await orchestrator.GetPodiumEntries(courseId);
    }
}
