using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.CompareAthletes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
	public class CompareAthletesApiRequest
	{
		public int[] AthleteIds { get; set; }
	}

	[Route("[controller]")]
	public class CompareAthletesApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public CompareAthletesApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpPost]
		public async Task<List<CompareAthletesAthleteInfoDto>> Post([FromBody]CompareAthletesApiRequest compareIrpApiRequest)
		{
			var orchestrator = new CompareAthletesOrchestrator(_scoringDbContext);
			return await orchestrator.GetCompareAthletesDto(compareIrpApiRequest.AthleteIds);
		}
	}
}
