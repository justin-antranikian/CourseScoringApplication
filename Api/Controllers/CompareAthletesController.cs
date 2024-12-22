using Api.DataModels;
using Api.Orchestration.Athletes.Compare;
using Microsoft.AspNetCore.Mvc;


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
        return await orchestrator.GetCompareAthletesDto(compareIrpApiRequest.AthleteIds.Take(4).ToList());
    }
}
