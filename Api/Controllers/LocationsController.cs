using Api.DataModels;
using Api.Orchestration.Locations.GetDetails;
using Api.Orchestration.Locations.GetDirectory;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("locations")]
public class LocationsController(ScoringDbContext dbContext) : ControllerBase
{
    [HttpGet("directory")]
    public async Task<List<DirectoryDto>> Get([FromQuery] int? locationId)
    {
        var orchestrator = new GetDirectoryOrchestrator(dbContext, locationId);
        return await orchestrator.Get();
    }

    [HttpGet("by-slug")]
    public async Task<IActionResult> Get([FromQuery] string slug)
    {
        var orchestrator = new GetLocationDetailOrchestrator(dbContext);
        return await orchestrator.Get(slug);
    }
}