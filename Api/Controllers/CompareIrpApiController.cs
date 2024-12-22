using Api.DataModels;
using Api.Orchestration.CompareIrps;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

public record CompareIrpApiRequest
{
    public required List<int> AthleteCourseIds { get; init; }
}

[Route("[controller]")]
public class CompareIrpApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpPost]
    public async Task<List<CompareIrpsAthleteInfoDto>> Post([FromBody] CompareIrpApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareIrpsOrchestrator(scoringDbContext);
        return await orchestrator.GetCompareIrpsDto(compareIrpApiRequest.AthleteCourseIds.Take(4).ToList());
    }
}
