using Microsoft.AspNetCore.Mvc;
using DataModels;
using Orchestration.GetAwardsPodium;

namespace Api.Controllers;

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
