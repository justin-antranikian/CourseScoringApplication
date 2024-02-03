﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetSearchAllEntitiesSearch;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class SearchAllEntitiesSearchApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    public async Task<AllEntitiesSearchResultDto> Get([FromQuery] string searchTerm)
    {
        var orchestrator = new SearchAllEntitiesOrchestrator(_scoringDbContext);
        return await orchestrator.GetSearchResults(searchTerm);
    }
}
