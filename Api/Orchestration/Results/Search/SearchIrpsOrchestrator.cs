using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Results.Search;

public class SearchIrpsOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<IrpSearchResult>> Get(IrpSearchRequest request)
    {
        var searchTerm = request.SearchTerm.ToLower();

        var query = scoringDbContext.AthleteCourses
            .Include(oo => oo.Athlete)
            .Include(oo => oo.Course)
            .Where(oo => oo.Course.RaceId == request.RaceId);

        query = query.Where(oo => oo.Bib.Contains(searchTerm) || oo.Athlete.FullName.Contains(searchTerm));

        if (request.CourseId.HasValue)
        {
            query = query.Where(oo => oo.CourseId == request.CourseId);
        }

        var results = await query.ToListAsync();
        return results.Select(MapToDto).ToList();
    }

    private static IrpSearchResult MapToDto(AthleteCourse athleteCourse)
    {
        var athlete = athleteCourse.Athlete;
        return new IrpSearchResult
        {
            Id = athleteCourse.Id,
            AthleteId = athleteCourse.AthleteId,
            CourseId = athleteCourse.Course.Id,
            Bib = athleteCourse.Bib,
            CourseName = athleteCourse.Course.Name,
            FirstName = athlete.FirstName,
            Gender = athlete.Gender.ToAbbreviation(),
            LastName = athlete.LastName,
        };
    }
}
