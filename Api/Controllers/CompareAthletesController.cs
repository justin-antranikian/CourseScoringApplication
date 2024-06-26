﻿using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.CompareAthletes;

namespace Api.Controllers;

public class CompareAthletesApiRequest
{
    public int[] AthleteIds { get; set; }
}

[Route("[controller]")]
public class CompareAthletesApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpPost]
    public async Task<List<CompareAthletesAthleteInfoDto>> Post([FromBody]CompareAthletesApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareAthletesOrchestrator(_scoringDbContext);
        return await orchestrator.GetCompareAthletesDto(compareIrpApiRequest.AthleteIds);
    }
}
