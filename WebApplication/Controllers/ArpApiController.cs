using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetArp;

namespace WebApplicationSandbox.Controllers
{
	[Route("[controller]")]
	public class ArpApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public ArpApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpGet]
		[Route("{athleteId:int}")]
		public async Task<ArpDto> Get(int athleteId)
		{
			var orchestrator = new GetArpOrchestrator(_scoringDbContext);
			return await orchestrator.GetArpDto(athleteId);
		}
	}
}
