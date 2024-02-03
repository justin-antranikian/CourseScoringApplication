using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetCourseInfo;
using System.Threading.Tasks;

namespace WebApplication.Controllers;

[Route("[controller]")]
public class GetCourseInfoApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    [Route("{courseId:int}")]
    public async Task<CourseInfoDto> Get(int courseId)
    {
        var orchestrator = new GetCourseInfoOrchestrator(_scoringDbContext);
        return await orchestrator.GetCourseInfo(courseId);
    }
}
