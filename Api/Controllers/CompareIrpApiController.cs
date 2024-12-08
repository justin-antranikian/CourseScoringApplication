using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.CompareIrps;

namespace Api.Controllers;

public class CompareIrpApiRequest
{
    public List<int> AthleteCourseIds { get; set; }
}

[Route("[controller]")]
public class CompareIrpApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpPost]
    public async Task<List<CompareIrpsAthleteInfoDto>> Post([FromBody] CompareIrpApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareIrpsOrchestrator(scoringDbContext);
        return await orchestrator.GetCompareIrpsDto(compareIrpApiRequest.AthleteCourseIds);
    }
}
