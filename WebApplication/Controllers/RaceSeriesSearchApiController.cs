using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Orchestration.GetRaceSeriesSearch;
using System.Collections.Generic;
using System.Linq;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class RaceSeriesSearchApiController : ControllerBase
{
	private readonly ScoringDbContext _scoringDbContext;

	private readonly IMemoryCache _memoryCache;

	private static readonly object _lock = new();

	public RaceSeriesSearchApiController(ScoringDbContext scoringDbContext, IMemoryCache memoryCache)
    {
        _scoringDbContext = scoringDbContext;
        _memoryCache = memoryCache;
    }

	private List<EventSearchResultDto> GetSearchResults()
    {
		return _memoryCache.Get<List<EventSearchResultDto>>("allEvents");
	}

    [HttpGet]
    public List<EventSearchResultDto> Get([FromQuery] SearchEventsRequestDto requestDto)
    {
        var eventFromCache = GetSearchResults();
        if (eventFromCache != null)
        {
            return eventFromCache;
        }

        //var morex = 1;
        //foreach (var more in Enumerable.Range(1, 500000000))
        //{
        //    morex = more;
        //}

        List<EventSearchResultDto> searchResults = null;
        lock (_lock)
        {
            var fromCache = GetSearchResults();
            if (fromCache != null)
            {
                return eventFromCache;
            }

            var orchestrator = new SearchEventsOrchestrator(_scoringDbContext);
            searchResults = orchestrator.GetSearchResults(requestDto).GetAwaiter().GetResult();
            _memoryCache.Set("allEvents", searchResults);
        }

        return searchResults;
    }
}
