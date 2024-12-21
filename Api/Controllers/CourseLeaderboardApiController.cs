using Api.DataModels;
using Api.Orchestration.GetLeaderboard.GetCourseLeaderboard;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class CourseLeaderboardApiController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet]
    [Route("{courseId:int}")]
    public async Task<CourseLeaderboardDto> Get(int courseId, int? bracketId, int? intervalId, int startingRank = 1, int take = 50)
    {
        var orchestrator = new GetCourseLeaderboardOrchestrator(scoringDbContext);
        return await orchestrator.GetCourseLeaderboardDto(courseId, bracketId, intervalId, startingRank, take);
    }
}
