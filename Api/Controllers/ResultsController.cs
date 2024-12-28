using Api.DataModels;
using Api.Orchestration.Results.Compare;
using Api.Orchestration.Results.GetDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public record CompareIrpApiRequest
{
    public required List<int> AthleteCourseIds { get; init; }
}

public record ResultSearchRequest
{
    public int? CourseId { get; init; }
    public required int RaceId { get; init; }
    public required string SearchTerm { get; init; }
}

public record ResultSearchResult
{
    public required int Id { get; init; }
    public required int AthleteId { get; init; }
    public required int CourseId { get; init; }
    public required string CourseName { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Bib { get; init; }
}

[ApiController]
[Route("results")]
public class ResultsController(ScoringDbContext scoringDbContext) : ControllerBase
{
    [HttpGet("{athleteCourseId:int}")]
    public async Task<IrpDto> Get([FromRoute] int athleteCourseId)
    {
        var orchestrator = new GetIrpOrchestrator(scoringDbContext);
        return await orchestrator.GetIrpDto(athleteCourseId);
    }

    [HttpPost("compare")]
    public async Task<List<CompareIrpsAthleteInfoDto>> Post([FromBody] CompareIrpApiRequest compareIrpApiRequest)
    {
        var orchestrator = new CompareIrpsOrchestrator(scoringDbContext);
        return await orchestrator.GetCompareIrpsDto(compareIrpApiRequest.AthleteCourseIds.Take(4).ToList());
    }

    [HttpPost("search")]
    public async Task<List<ResultSearchResult>> Search([FromBody] ResultSearchRequest request)
    {
        var searchTerm = request.SearchTerm.ToLower();
        var query = scoringDbContext.AthleteCourses
            .Include(oo => oo.Athlete)
            .Include(oo => oo.Course)
            .Where(oo => oo.Course.RaceId == request.RaceId);

        query = query.Where(oo => oo.Bib.Contains(searchTerm) || oo.Athlete.FirstName.Contains(searchTerm) || oo.Athlete.LastName.Contains(searchTerm));

        if (request.CourseId.HasValue)
        {
            query = query.Where(oo => oo.CourseId == request.CourseId);
        }

        var results = await query.ToListAsync();

        return results.Select(oo => new ResultSearchResult
        {
            Id = oo.Id,
            Bib = oo.Bib,
            FirstName = oo.Athlete.FirstName,
            LastName = oo.Athlete.LastName,
            CourseId = oo.Course.Id,
            CourseName = oo.Course.Name,
            AthleteId = oo.AthleteId
        }).ToList();
    }
}