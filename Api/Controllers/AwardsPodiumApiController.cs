using Api.DataModels;
using Api.Orchestration.Courses.GetAwardsPodium;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class AwardsPodiumApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{courseId:int}")]
    public async Task<List<PodiumEntryDto>> Get([FromRoute] int courseId)
    {
        var orchestrator = new GetAwardsPodiumOrchestrator(scoringDbContext);
        return await orchestrator.GetPodiumEntries(courseId);
    }
}
