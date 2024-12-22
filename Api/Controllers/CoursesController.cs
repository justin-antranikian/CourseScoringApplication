using Api.DataModels;
using Api.Orchestration.Courses.GetAwardsPodium;
using Api.Orchestration.Courses.GetLeaderboard;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet("{courseId:int}")]
    public async Task<CourseLeaderboardDto> Get(int courseId, int? bracketId, int? intervalId, int startingRank = 1, int take = 50)
    {
        var orchestrator = new GetCourseLeaderboardOrchestrator(scoringDbContext);
        return await orchestrator.GetCourseLeaderboardDto(courseId, bracketId, intervalId, startingRank, take);
    }

    [HttpGet("{courseId:int}/awards")]
    public async Task<List<PodiumEntryDto>> Awards([FromRoute] int courseId)
    {
        var orchestrator = new GetAwardsPodiumOrchestrator(scoringDbContext);
        return await orchestrator.GetPodiumEntries(courseId);
    }
}