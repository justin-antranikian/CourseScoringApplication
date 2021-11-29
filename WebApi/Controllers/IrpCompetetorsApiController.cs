using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetCompetetorsForIrp;
using System.Threading.Tasks;
using static Orchestration.GetCompetetorsForIrp.IrpCompetetorDto;

namespace WebApplication.Controllers
{
	[Route("[controller]")]
	public class IrpCompetetorsApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public IrpCompetetorsApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpGet]
		[Route("{athleteCourseId:int}")]
		public async Task<GetCompetetorsForIrpDto> Get(int athleteCourseId)
		{
			var orchestrator = new GetCompetetorsForIrpOrchestrator(_scoringDbContext);
			return await orchestrator.GetCompetetorsForIrpResult(athleteCourseId);
		}
	}
}
