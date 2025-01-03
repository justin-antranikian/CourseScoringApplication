﻿using Api.DataModels;
using Api.Orchestration.Courses.GetAwards;
using Api.Orchestration.Courses.GetDetails;
using Api.Orchestration.Courses.GetLeaderboard;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController(ScoringDbContext dbContext) : ControllerBase
{
    [HttpGet("{courseId:int}")]
    public async Task<CourseDetailsDto> GetDetails([FromRoute] int courseId)
    {
        var orchestrator = new GetDetailsOrchestrator(dbContext);
        return await orchestrator.Get(courseId);
    }

    [HttpGet("{courseId:int}/leaderboard")]
    public async Task<List<CourseLeaderboard>> GetLeaderboard(int courseId, int? bracketId, int? intervalId, int startingRank = 1, int take = 50)
    {
        var orchestrator = new GetCourseLeaderboardOrchestrator(dbContext);
        return await orchestrator.Get(courseId, bracketId, intervalId, startingRank, take);
    }

    [HttpGet("{courseId:int}/awards")]
    public async Task<List<AwardsDto>> Awards([FromRoute] int courseId)
    {
        var orchestrator = new GetAwardsOrchestrator(dbContext);
        return await orchestrator.Get(courseId);
    }
}