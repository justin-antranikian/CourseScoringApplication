using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.Compare;

public class CompareAthletesOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<AthleteCompareDto>> GetCompareAthletesDto(List<int> athleteIds)
    {
        var athletes = await GetAthletes(athleteIds);
        var results = await GetResults(athleteIds);

        var goals = await dbContext.AthleteRaceSeriesGoals.Where(oo => athleteIds.Contains(oo.AthleteId)).ToListAsync();

        var distinctCourseIds = results.Select(oo => oo.CourseId).Distinct().ToArray();
        var courses = await GetCourses(distinctCourseIds);

        var dtos = new List<AthleteCompareDto>();
        foreach (var athlete in athletes)
        {
            var resultsForAthlete = results.Where(result => result.AthleteCourse.AthleteId == athlete.Id).ToList();
            var athleteGoals = goals.Where(oo => oo.AthleteId == athlete.Id).ToList();
            var dto = CompareAthletesAthleteInfoDtoMapper.GetCompareAthletesAthleteInfoDto(athlete, courses, resultsForAthlete, athleteGoals);
            dtos.Add(dto);
        }

        return dtos;
    }

    private async Task<List<Athlete>> GetAthletes(List<int> athleteIds)
    {
        var query = dbContext.Athletes
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .Where(oo => athleteIds.Contains(oo.Id))
            .OrderBy(oo => oo.FullName);

        return await query.ToListAsync();
    }

    private async Task<List<Result>> GetResults(List<int> athleteIds)
    {
        var query = dbContext.Results
                        .Include(oo => oo.AthleteCourse)
                        .Where(oo => athleteIds.Contains(oo.AthleteCourse!.AthleteId))
                        .Where(oo => oo.Bracket.BracketType == BracketType.Overall && oo.IsHighestIntervalCompleted);

        return await query.ToListAsync();
    }

    private async Task<List<Course>> GetCourses(int[] courseIds)
    {
        var query = dbContext.Courses
                        .Include(oo => oo.Race)
                        .ThenInclude(oo => oo.RaceSeries)
                        .Where(oo => courseIds.Contains(oo.Id));

        return await query.ToListAsync();
    }
}
