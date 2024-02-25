using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetCompetetorsForIrp;
using System.Threading.Tasks;

namespace WebApplication.Controllers;

[Route("[controller]")]
public class IrpCompetetorsApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{athleteCourseId:int}")]
    public async Task<GetCompetetorsForIrpDto> Get(int athleteCourseId)
    {
        var orchestrator = new GetCompetetorsForIrpOrchestrator(scoringDbContext);
        return await orchestrator.GetCompetetorsForIrpResult(athleteCourseId);
    }
}
