using Api.DataModels;
using Api.Orchestration.Results.Compare;
using Api.Orchestration.Results.GetDetails;
using Api.Orchestration.Results.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public record CompareIrpApiRequest
{
    public required List<int> AthleteCourseIds { get; init; }
}

[ApiController]
[Route("results")]
public class ResultsController(ScoringDbContext dbContext) : ControllerBase
{
    [HttpGet("{athleteCourseId:int}")]
    public async Task<IrpDto> Get([FromRoute] int athleteCourseId)
    {
        var orchestrator = new GetIrpOrchestrator(dbContext);
        return await orchestrator.GetIrpDto(athleteCourseId);
    }

    [HttpPost("compare")]
    public async Task<List<CompareIrpsAthleteInfoDto>> Post([FromBody] CompareIrpApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareIrpsOrchestrator(dbContext);
        return await orchestrator.GetCompareIrpsDto(compareIrpApiRequest.AthleteCourseIds.Take(4).ToList());
    }

    [HttpPost("search")]
    public async Task<List<IrpSearchResult>> Search([FromBody] IrpSearchRequest request)
    {
        var orchestrator = new SearchIrpsOrchestrator(dbContext);
        return await orchestrator.Get(request);
    }
}