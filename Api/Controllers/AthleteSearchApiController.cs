using Api.DataModels;
using Api.Orchestration.SearchAthletes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
