using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.CompareIrps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers;

public class CompareIrpApiRequest
{
    public List<int> AthleteCourseIds { get; set; }
}

[Route("[controller]")]
public class CompareIrpApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpPost]
    public async Task<List<CompareIrpsAthleteInfoDto>> Post([FromBody]CompareIrpApiRequest compareIrpApiRequest)
    {
            var orchestrator = new CompareIrpsOrchestrator(_scoringDbContext);
        return await orchestrator.GetCompareIrpsDto(compareIrpApiRequest.AthleteCourseIds);
    }
}
