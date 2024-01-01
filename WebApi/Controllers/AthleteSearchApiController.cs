using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using System.Collections.Generic;
using Orchestration.AthletesSearch;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace WebApplicationSandbox.Controllers
{
	[Route("[controller]")]
	public class AthleteSearchApiController : ControllerBase
	{
		private readonly ScoringDbContext _scoringDbContext;

		private readonly IMemoryCache _memoryCache;

		public AthleteSearchApiController(ScoringDbContext scoringDbContext, IMemoryCache memoryCache)
		{
			_scoringDbContext = scoringDbContext;
			_memoryCache = memoryCache;
		}

		private static object LockObject = new();

		[HttpGet]
		public async Task<List<AthleteSearchResultDto>> Get(SearchAthletesRequestDto searchRequestDto)
		{
			var fromCache = _memoryCache.Get<List<AthleteSearchResultDto>>("SeachResults");

			if (fromCache != null)
			{
				return fromCache;
			}

            var orchestrator = new SearchAthletesOrchestrator(_scoringDbContext);
			var results = await orchestrator.GetSearchResults(searchRequestDto);

			lock (LockObject)
			{
				if (_memoryCache.TryGetValue("SeachResults", out var _))
				{
                    _memoryCache.Set("SeachResults", results, TimeSpan.FromDays(1));
                }
            }

            return results;
		}
	}
}
