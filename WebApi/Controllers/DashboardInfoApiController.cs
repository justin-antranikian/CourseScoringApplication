using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetDashboardInfo;
using System.Threading.Tasks;

namespace WebApplicationSandbox.Controllers
{
	[Route("[controller]")]
	public class DashboardInfoApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public DashboardInfoApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpGet]
		public DashboardInfoResponseDto Get([FromQuery] DashboardInfoRequestDto requestDto)
		{
			var orchestrator = new GetDashboardInfoOrchestrator(_scoringDbContext);
			return orchestrator.GetResult(requestDto);
		}
	}
}
