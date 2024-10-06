using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.CompareAthletes;

namespace Api.Controllers;

public class CompareAthletesApiRequest
{
    public int[] AthleteIds { get; set; }
}

[Route("[controller]")]
public class CompareAthletesApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpPost]
    public async Task<List<CompareAthletesAthleteInfoDto>> Post([FromBody]CompareAthletesApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareAthletesOrchestrator(scoringDbContext);
        return await orchestrator.GetCompareAthletesDto(compareIrpApiRequest.AthleteIds);
    }
}
