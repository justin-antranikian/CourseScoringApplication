using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using System.Collections.Generic;
using Orchestration.AthletesSearch;

namespace WebApplicationSandbox.Controllers
{
	[Route("[controller]")]
	public class AthleteSearchApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		public AthleteSearchApiController(ScoringDbContext scoringDbContext)
		{
			_scoringDbContext = scoringDbContext;
		}

		[HttpGet]
		public async Task<List<AthleteSearchResultDto>> Get(SearchAthletesRequestDto searchRequestDto)
		{
			var orchestrator = new SearchAthletesOrchestrator(_scoringDbContext);
			return await orchestrator.GetSearchResults(searchRequestDto);
		}
	}
}
