using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetRaceSeriesDashboard;

namespace WebApplicationSandbox.Controllers
{
	[Route("[controller]")]
	public class RaceSeriesDashboardApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public RaceSeriesDashboardApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpGet]
		[Route("{raceSeriesId:int}")]
		public async Task<RaceSeriesDashboardDto> Get(int raceSeriesId)
		{
			var orchestrator = new GetRaceSeriesDashboardOrchestrator(_scoringDbContext);
			return await orchestrator.GetRaceSeriesDashboardDto(raceSeriesId);
		}
	}
}
