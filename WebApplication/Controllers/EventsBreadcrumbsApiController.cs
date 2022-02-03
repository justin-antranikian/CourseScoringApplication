using DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Orchestration.GetBreadcrumb;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class EventsBreadCrumbsApiController : ControllerBase
{
	private readonly ScoringDbContext _scoringDbContext;

	public EventsBreadCrumbsApiController(ScoringDbContext scoringDbContext)
	{
		_scoringDbContext = scoringDbContext;
	}

	[HttpGet]
	public async Task<EventsBreadcrumbResultDto> Get([FromQuery] BreadcrumbRequestDto requestDto)
	{
		var orchestrator = new GetEventsBreadcrumbOrchestrator(_scoringDbContext);
		return await orchestrator.GetResult(requestDto);
	}
}
