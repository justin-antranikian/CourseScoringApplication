using DataModels;
using Microsoft.AspNetCore.Mvc;
using Orchestration.GetLeaderboard.GetCourseLeaderboard;
using System.Threading.Tasks;

namespace WebApplicationSandbox.Controllers;

[Route("[controller]")]
public class CourseLeaderboardApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    private readonly ScoringDbContext _scoringDbContext = scoringDbContext;

    [HttpGet]
    [Route("{courseId:int}")]
    public async Task<CourseLeaderboardDto> Get(int courseId, int? bracketId, int? intervalId, int startingRank = 1, int take = 50)
    {
        var orchestrator = new GetCourseLeaderboardOrchestrator(_scoringDbContext);
        return await orchestrator.GetCourseLeaderboardDto(courseId, bracketId, intervalId, startingRank, take);
    }
}
