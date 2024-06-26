﻿using Microsoft.AspNetCore.Mvc;
using DataModels;
using Orchestration.AthletesSearch;

namespace Api.Controllers;

[Route("[controller]")]
public class AthleteSearchApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    public async Task<List<AthleteSearchResultDto>> Get(SearchAthletesRequestDto searchRequestDto)
    {
        var orchestrator = new SearchAthletesOrchestrator(scoringDbContext);
        var results = await orchestrator.GetSearchResults(searchRequestDto);
        return results;
    }
}
