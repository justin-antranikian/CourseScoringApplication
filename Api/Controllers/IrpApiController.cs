using Microsoft.AspNetCore.Mvc;
using DataModels;
using Orchestration.GetIrp;

namespace Api.Controllers;

[Route("[controller]")]
public class IrpApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{athleteCourseId:int}")]
    public async Task<IrpDto> Get(int athleteCourseId)
    {
        var orchestrator = new GetIrpOrchestrator(scoringDbContext);
        return await orchestrator.GetIrpDto(athleteCourseId);
    }
}
