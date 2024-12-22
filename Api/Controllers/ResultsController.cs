using Api.DataModels;
using Api.Orchestration.Results.Compare;
using Api.Orchestration.Results.GetDetails;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public record CompareIrpApiRequest
{
    public required List<int> AthleteCourseIds { get; init; }
}

[ApiController]
[Route("results")]
public class ResultsController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet("{athleteCourseId:int}")]
    public async Task<IrpDto> Get([FromRoute] int athleteCourseId)
    {
        var orchestrator = new GetIrpOrchestrator(scoringDbContext);
        return await orchestrator.GetIrpDto(athleteCourseId);
    }

    [HttpPost("compare")]
    public async Task<List<CompareIrpsAthleteInfoDto>> Post([FromBody] CompareIrpApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareIrpsOrchestrator(scoringDbContext);
        return await orchestrator.GetCompareIrpsDto(compareIrpApiRequest.AthleteCourseIds.Take(4).ToList());
    }
}