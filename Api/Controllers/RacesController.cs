using Api.DataModels;
using Api.Orchestration.Races.GetLeaderboard;
using Api.Orchestration.Races.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("races")]
public class RacesController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet("{raceId:int}")]
    public async Task<RaceLeaderboardDto> Get([FromRoute] int raceId)
    {
        var orchestrator = new GetRaceLeaderboardOrchestrator(scoringDbContext);
        return await orchestrator.GetRaceLeaderboardDto(raceId);
    }

    [HttpGet("search")]
    public async Task<List<EventSearchResultDto>> Get([FromQuery] SearchEventsRequestDto requestDto)
    {
        var orchestrator = new SearchRacesOrchestrator(scoringDbContext);
        return await orchestrator.Get(requestDto);
    }

    [HttpGet("by-slug")]
    public async Task<IActionResult> GetByLocation([FromQuery] string slug)
    {
        var location = await scoringDbContext.Locations.SingleOrDefaultAsync(oo => oo.Slug == slug.ToLower());

        if (location == null)
        {
            return NotFound($"Could not find location by slug. Slug: ({slug}).");
        }

        var baseQuery = scoringDbContext.RaceSeries
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .Include(oo => oo.Races)
            .ThenInclude(oo => oo.Courses)
            .Where(oo => oo.StateLocationId == location.Id || oo.AreaLocationId == location.Id || oo.CityLocationId == location.Id);

        var athletes = await baseQuery.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
        var results = athletes.Select(EventSearchResultDtoMapper.GetEventSearchResultDto).ToList();
        return Ok(results);
    }

}