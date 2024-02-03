using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetDashboardInfo;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class DashboardInfoApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    public DashboardInfoResponseDto Get([FromQuery] DashboardInfoRequestDto requestDto)
    {
        var orchestrator = new GetDashboardInfoOrchestrator(_scoringDbContext);
        return orchestrator.GetResult(requestDto);
    }
}
