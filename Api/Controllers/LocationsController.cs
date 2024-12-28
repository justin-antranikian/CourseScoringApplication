using Api.DataModels;
using Api.Orchestration.Locations;
using Api.Orchestration.Locations.GetDetails;
using Api.Orchestration.Locations.GetDirectory;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("locations")]
public class LocationsController(ScoringDbContext dbContext) : ControllerBase
{
    [HttpGet("directory")]
    public async Task<List<LocationDto>> Get([FromQuery] int? locationId)
    {
        var orchestrator = new GetDirectoryOrchestrator(dbContext);
        return await orchestrator.Get(locationId);
    }

    [HttpGet("by-slug")]
    public async Task<IActionResult> Get([FromQuery] string slug)
    {
        var orchestrator = new GetLocationDetailOrchestrator(dbContext);
        return await orchestrator.Get(slug);
    }
}