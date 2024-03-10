using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetDashboardInfo;

namespace Api.Controllers;

[Route("[controller]")]
public class DashboardInfoApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public DashboardInfoResponseDto Get([FromQuery] DashboardInfoRequestDto requestDto)
    {
        var orchestrator = new GetDashboardInfoOrchestrator(scoringDbContext);
        return orchestrator.GetResult(requestDto);
    }
}
