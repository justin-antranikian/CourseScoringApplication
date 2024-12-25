using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.GetDetails;

public class GetArpRepository(ScoringDbContext scoringDbContext)
{
    public record QueryResult(List<ResultWithBracketType> Results, List<Course> Courses, List<BracketMetadata> MetadataEntries, Athlete Athlete);

    public async Task<QueryResult> GetQueryResults(int athleteId)
    {
        var athlete = await scoringDbContext.Athletes
                                .Include(oo => oo.AthleteWellnessEntries)
                                .Include(oo => oo.AthleteRaceSeriesGoals)
                                .Include(oo => oo.AreaLocation)
                                .Include(oo => oo.CityLocation)
                                .Include(oo => oo.StateLocation)
                                .AsSplitQuery()
                                .SingleAsync(oo => oo.Id == athleteId);

        var results = await GetResults(athleteId);
        var courses = await GetCourses(results);
        var bracketMetadataEntries = await GetBracketMetadataEntries(results);
        return new QueryResult(results, courses, bracketMetadataEntries, athlete);
    }

    private async Task<List<ResultWithBracketType>> GetResults(int athleteId)
    {
        var bracketTypes = new[] { BracketType.Overall, BracketType.Gender, BracketType.PrimaryDivision };

        var query = from results in scoringDbContext.Results
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
        var query = scoringDbContext.Courses
                        .Include(oo => oo.Brackets)
                        .Include(oo => oo.Intervals)
                        .Include(oo => oo.Race)
                        .ThenInclude(oo => oo!.RaceSeries)
                        .AsSplitQuery()
                        .Where(oo => courseIds.Contains(oo.Id));

        return await query.ToListAsync();
    }

    public async Task<List<BracketMetadata>> GetBracketMetadataEntries(List<ResultWithBracketType> results)
    {
        var bracketIds = results.Select(oo => oo.BracketId).ToList();
        var query = scoringDbContext.BracketMetadataEntries.Where(oo => bracketIds.Contains(oo.BracketId) && oo.IntervalId == null);
        return await query.ToListAsync();
    }
}
