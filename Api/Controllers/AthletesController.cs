using Api.DataModels;
using Api.Orchestration.Athletes.Compare;
using Api.Orchestration.Athletes.GetDetails;
using Api.Orchestration.Athletes.Search;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("athletes")]
public class AthletesController(ScoringDbContext dbContext) : ControllerBase
{
    [HttpGet("{athleteId:int}")]
    public async Task<ArpDto> Details([FromRoute] int athleteId)
    {
        var orchestrator = new GetDetailsOrchestrator(dbContext);
        return await orchestrator.Get(athleteId);
    }

    [HttpPost("compare")]
    public async Task<List<AthleteCompareDto>> Compare([FromBody] int[] athleteIds)
    {
        var orchestrator = new CompareAthletesOrchestrator(dbContext);
        return await orchestrator.GetCompareAthletesDto(athleteIds.Take(4).ToList());
    }

    [HttpGet("search")]
    public async Task<List<AthleteSearchResultDto>> Search([FromQuery] int? locationId, [FromQuery] string? locationType, [FromQuery] string? searchTerm)
    {
        var orchestrator = new SearchAthletesOrchestrator(dbContext);
        return await orchestrator.Get(locationId, locationType, searchTerm);
    }
}