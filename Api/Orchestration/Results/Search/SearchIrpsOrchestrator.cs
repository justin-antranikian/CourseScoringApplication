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

        query = query.Where(oo => oo.Bib.Contains(searchTerm) || oo.Athlete.FirstName.Contains(searchTerm) || oo.Athlete.LastName.Contains(searchTerm));

        if (request.CourseId.HasValue)
        {
            query = query.Where(oo => oo.CourseId == request.CourseId);
        }

        var results = await query.ToListAsync();
        return results.Select(MapToDto).ToList();
    }

    private static IrpSearchResult MapToDto(AthleteCourse oo)
    {
        return new IrpSearchResult
        {
            Id = oo.Id,
            AthleteId = oo.AthleteId,
            CourseId = oo.Course.Id,
            Bib = oo.Bib,
            CourseName = oo.Course.Name,
            FirstName = oo.Athlete.FirstName,
            LastName = oo.Athlete.LastName,
        };
    }
}
