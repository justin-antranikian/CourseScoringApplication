using Api.DataModels;
using Api.Orchestration.Athletes.Compare;
using Api.Orchestration.Athletes.GetDetails;
using Api.Orchestration.Athletes.Search;
using Microsoft.AspNetCore.Mvc;

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

    //[HttpGet("search")]
    //public async Task<List<AthleteSearchResultDto>> Search([FromQuery] SearchAthletesRequestDto searchRequestDto)
    //{
    //    var orchestrator = new SearchAthletesOrchestrator(scoringDbContext);
    //    var results = await orchestrator.GetSearchResults(searchRequestDto);
    //    return results;
    //}
}