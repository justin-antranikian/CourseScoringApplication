using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.Compare;

public class CompareAthletesOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<CompareAthletesAthleteInfoDto>> GetCompareAthletesDto(List<int> athleteIds)
    {
        var athletes = await GetAthletes(athleteIds);
        var results = await GetResults(athleteIds);

        var goals = await scoringDbContext.AthleteRaceSeriesGoals.Where(oo => athleteIds.Contains(oo.AthleteId)).ToListAsync();

        var distinctCourseIds = results.Select(oo => oo.CourseId).Distinct().ToArray();
        var courses = await GetCourses(distinctCourseIds);

        var helper = new CompareAthletesHelper(courses, athletes, results, goals);
        return helper.GetMappedResults();
    }

    private async Task<List<Athlete>> GetAthletes(List<int> athleteIds)
    {
        var query = scoringDbContext.Athletes.Where(oo => athleteIds.Contains(oo.Id)).OrderBy(oo => oo.FullName);
        return await query.ToListAsync();
    }

    private async Task<List<Result>> GetResults(List<int> athleteIds)
    {
        var query = scoringDbContext.Results
                        .Include(oo => oo.AthleteCourse)
                        .Where(oo => athleteIds.Contains(oo.AthleteCourse.AthleteId))
                        .Where(oo => oo.Bracket.BracketType == BracketType.Overall && oo.IsHighestIntervalCompleted);

        return await query.ToListAsync();
    }

    private async Task<List<Course>> GetCourses(int[] courseIds)
    {
        var query = scoringDbContext.Courses
                        .Include(oo => oo.Race)
                        .ThenInclude(oo => oo.RaceSeries)
                        .Where(oo => courseIds.Contains(oo.Id));

        return await query.ToListAsync();
    }
}
