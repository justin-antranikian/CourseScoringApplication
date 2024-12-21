using Api.DataModels;
using Api.Orchestration.GetDashboardInfo;
using Microsoft.AspNetCore.Mvc;


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
