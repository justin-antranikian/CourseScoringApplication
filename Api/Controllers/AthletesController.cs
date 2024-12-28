using Api.DataModels;
using Api.Orchestration.Athletes.Compare;
using Api.Orchestration.Athletes.GetDetails;
using Api.Orchestration.Athletes.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class CompareAthletesApiRequest
{
    public int[] AthleteIds { get; set; }
}

[ApiController]
[Route("athletes")]
public class AthletesController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet("{athleteId:int}")]
    public async Task<ArpDto> Details([FromRoute] int athleteId)
    {
        var orchestrator = new GetAthleteDetailsOrchestrator(scoringDbContext);
        return await orchestrator.GetArpDto(athleteId);
    }

    [HttpPost("compare")]
    public async Task<List<CompareAthletesAthleteInfoDto>> Compare([FromBody] CompareAthletesApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareAthletesOrchestrator(scoringDbContext);
        return await orchestrator.GetCompareAthletesDto(compareIrpApiRequest.AthleteIds.Take(4).ToList());
    }

    [HttpGet("search")]
    public async Task<List<AthleteSearchResultDto>> Search([FromQuery] SearchAthletesRequestDto searchRequestDto)
    {
        var orchestrator = new SearchAthletesOrchestrator(scoringDbContext);
        return await orchestrator.GetSearchResults(searchRequestDto);
    }

    [HttpGet("by-slug")]
    public async Task<IActionResult> GetByLocation([FromQuery] string slug)
    {
        var location = await scoringDbContext.Locations.SingleOrDefaultAsync(oo => oo.Slug == slug.ToLower());

        if (location == null)
        {
            return NotFound($"Could not find location by slug. Slug: ({slug}).");
        }

        var baseQuery = scoringDbContext.Athletes
            .Include(oo => oo.AthleteRaceSeriesGoals)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .Include(oo => oo.StateLocation)
            .Where(oo => oo.StateLocationId == location.Id || oo.AreaLocationId == location.Id || oo.CityLocationId == location.Id);

        var athletes = await baseQuery.OrderBy(oo => oo.OverallRank).Take(28).ToListAsync();
        var results = athletes.Select(AthleteSearchResultDtoMapper.GetAthleteSearchResultDto).ToList();
        return Ok(results);
    }
}