using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.Compare;

public class CompareAthletesOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<AthleteCompareDto>> GetCompareAthletesDto(List<int> athleteIds)
    {
        var athletes = await dbContext.GetAthletesWithLocationInfo().Where(oo => athleteIds.Contains(oo.Id)).OrderBy(oo => oo.FullName).ToListAsync();
        var results = await GetResults(athleteIds);

        var goals = await dbContext.AthleteRaceSeriesGoals.Where(oo => athleteIds.Contains(oo.AthleteId)).ToListAsync();

        var courseIds = results.Select(oo => oo.CourseId).Distinct().ToArray();
        var courses = await dbContext.Courses.Include(oo => oo.Race).ThenInclude(oo => oo.RaceSeries).Where(oo => courseIds.Contains(oo.Id)).ToListAsync();

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

    private async Task<List<Result>> GetResults(List<int> athleteIds)
    {
        return await dbContext.Results
            .Include(oo => oo.AthleteCourse)
            .Where(oo => athleteIds.Contains(oo.AthleteCourse!.AthleteId))
            .Where(oo => oo.Bracket.BracketType == BracketType.Overall && oo.IsHighestIntervalCompleted)
            .ToListAsync();
    }
}
