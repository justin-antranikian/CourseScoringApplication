using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetIrp;

namespace WebApplicationSandbox.Controllers;

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
