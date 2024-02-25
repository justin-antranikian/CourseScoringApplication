using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using System.Collections.Generic;
using Orchestration.AthletesSearch;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class AthleteSearchApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<List<AthleteSearchResultDto>> Get(SearchAthletesRequestDto searchRequestDto)
    {
        var orchestrator = new SearchAthletesOrchestrator(scoringDbContext);
        var results = await orchestrator.GetSearchResults(searchRequestDto);
        return results;
    }
}
