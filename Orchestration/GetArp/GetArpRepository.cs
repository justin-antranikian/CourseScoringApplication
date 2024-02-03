using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.GetArp;

public class GetArpRepository
{
    public record QueryResult(List<ResultWithBracketType> Results, List<Course> Courses, List<BracketMetadata> MetadataEntries, Athlete Athlete);

    private readonly ScoringDbContext _scoringDbContext;

    public GetArpRepository(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task<QueryResult> GetQueryResults(int athleteId)
    {
        var athlete = await _scoringDbContext.Athletes
                                .Include(oo => oo.AthleteWellnessEntries)
                                .Include(oo => oo.AthleteRelationshipEntries)
                                .Include(oo => oo.AthleteRaceSeriesGoals)
                                .SingleAsync(oo => oo.Id == athleteId);

        var results = await GetResults(athleteId);
        var courses = await GetCourses(results);
        var bracketMetadataEntries = await GetBracketMetadataEntries(results);
        return new QueryResult(results, courses, bracketMetadataEntries, athlete);
    }

    private async Task<List<ResultWithBracketType>> GetResults(int athleteId)
    {
        var bracketTypes = new[] { BracketType.Overall, BracketType.Gender, BracketType.PrimaryDivision };

        var query = from results in _scoringDbContext.Results
                    where
                        bracketTypes.Contains(results.Bracket.BracketType) &&
                        results.AthleteCourse.AthleteId == athleteId &&
                        results.IsHighestIntervalCompleted == true
                    select new ResultWithBracketType
                    (
                        results.AthleteCourseId,
                        results.BracketId,
                        results.Bracket.BracketType,
                        results.CourseId,
                        results.IntervalId,
                        results.TimeOnCourse,
                        results.OverallRank,
                        results.GenderRank,
                        results.DivisionRank
                    );

        return await query.ToListAsync();
    }

    public async Task<List<Course>> GetCourses(List<ResultWithBracketType> results)
    {
        var courseIds = results.Select(oo => oo.CourseId).Distinct().ToList();
        var query = _scoringDbContext.Courses
                        .Include(oo => oo.Brackets)
                        .Include(oo => oo.Intervals)
                        .Include(oo => oo.Race)
                        .ThenInclude(oo => oo.RaceSeries)
                        .Where(oo => courseIds.Contains(oo.Id));

        return await query.ToListAsync();
    }

    public async Task<List<BracketMetadata>> GetBracketMetadataEntries(List<ResultWithBracketType> results)
    {
        var bracketIds = results.Select(oo => oo.BracketId).ToList();
        var query = _scoringDbContext.BracketMetadataEntries.Where(oo => bracketIds.Contains(oo.BracketId) && oo.IntervalId == null);
        return await query.ToListAsync();
    }
}
