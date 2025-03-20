using Api.DataModels;
using Api.Orchestration;
using Api.Orchestration.Races.GetLeaderboard;
using Api.Orchestration.Races.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NetTopologySuite;

namespace Api.Controllers;

[ApiController]
[Route("races")]
public class RacesController(ScoringDbContext dbContext) : ControllerBase
{
    [HttpGet("{raceId:int}")]
    public async Task<RaceLeaderboardDto> Get([FromRoute] int raceId)
    {
        var orchestrator = new GetRaceLeaderboardOrchestrator(dbContext);
        return await orchestrator.GetRaceLeaderboardDto(raceId);
    }

    [HttpGet("search")]
    public async Task<List<RaceSearchResultDto>> Get([FromQuery] SearchRacesRequest request)
    {
        var orchestrator = new SearchRacesOrchestrator(dbContext);
        return await orchestrator.Get(request);
    }

    [HttpGet("search2")]
    public async Task<IActionResult> Get()
    {
        var raceSeries = dbContext.RaceSeries.First(oo => oo.CityLocation!.Name == "Denver");

        var latitude = raceSeries.Location.Coordinate.Y;
        var longitude = raceSeries.Location.Coordinate.X;

        var point = GeometryExtensions.CreatePoint(latitude, longitude, 0);
        var intersectingAthletes = await dbContext.Athletes.Where(oo => oo.Location.Intersects(point)).ToListAsync();
        return Ok(intersectingAthletes.Select(oo => new { oo.Id, oo.FirstName, oo.LastName }));
    }
}