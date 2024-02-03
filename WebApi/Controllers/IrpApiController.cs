﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataModels;
using Orchestration.GetIrp;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class IrpApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    [Route("{athleteCourseId:int}")]
    public async Task<IrpDto> Get(int athleteCourseId)
    {
        var orchestrator = new GetIrpOrchestrator(_scoringDbContext);
        return await orchestrator.GetIrpDto(athleteCourseId);
    }
}
