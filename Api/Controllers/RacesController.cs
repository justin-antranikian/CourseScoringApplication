using Api.DataModels;
using Api.Orchestration.Races.GetLeaderboard;
using Api.Orchestration.Races.Search;
using Microsoft.AspNetCore.Mvc;

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
}