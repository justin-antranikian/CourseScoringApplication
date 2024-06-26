﻿using Microsoft.AspNetCore.Mvc;
using Orchestration.GetBreadcrumb;

namespace Api.Controllers;

[Route("[controller]")]
public class AthletesBreadCrumbsApiController : ControllerBase
{
    [HttpGet]
    public BreadcrumbResultDto Get([FromQuery] BreadcrumbRequestDto requestDto)
    {
        var orchestrator = new GetAthleteBreadcrumbOrchestrator();
        return orchestrator.GetResult(requestDto);
    }
}
