﻿using Api.DataModels;
using Api.Orchestration.Races.GetLeaderboard;
using Api.Orchestration.Races.Search;
using Microsoft.AspNetCore.Mvc;

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
}