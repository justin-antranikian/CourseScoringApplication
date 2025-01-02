using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.Compare;

public class CompareAthletesOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<AthleteCompareDto>> GetCompareAthletesDto(List<int> athleteIds)
    {
        var athletes = await dbContext.GetAthletesWithLocationInfo().Where(oo => athleteIds.Contains(oo.Id)).OrderBy(oo => oo.FullName).ToListAsync();

        var results = await dbContext.Results
            .Include(oo => oo.AthleteCourse)
            .Where(oo => athleteIds.Contains(oo.AthleteCourse!.AthleteId))
            .Where(oo => oo.Bracket.BracketType == BracketType.Overall && oo.IsHighestIntervalCompleted)
            .ToListAsync();

        var goals = await dbContext.AthleteRaceSeriesGoals.Where(oo => athleteIds.Contains(oo.AthleteId)).ToListAsync();

        var courseIds = results.Select(oo => oo.CourseId).Distinct().ToArray();
        var courses = await dbContext.Courses.Include(oo => oo.Race).ThenInclude(oo => oo.RaceSeries).Where(oo => courseIds.Contains(oo.Id)).ToListAsync();

        return MapToCompareResults(athletes, results, goals, courses).ToList();
    }

    private static IEnumerable<AthleteCompareDto> MapToCompareResults(List<Athlete> athletes, List<Result> results, List<AthleteRaceSeriesGoal> goals, List<Course> courses)
    {
        foreach (var athlete in athletes)
        {
            var resultsForAthlete = results.Where(result => result.AthleteCourse.AthleteId == athlete.Id).ToList();
            var athleteGoals = goals.Where(oo => oo.AthleteId == athlete.Id).ToList();
            yield return MapToDto(athlete, courses, resultsForAthlete, athleteGoals);
        }
    }

    private static AthleteCompareDto MapToDto(Athlete athlete, List<Course> courses, List<Result> resultsForAthlete, List<AthleteRaceSeriesGoal> goals)
    {
        AthleteCompareStatDto GetCompareAthletesStat(IGrouping<RaceSeriesType, Course> raceSeriesTypeGrouping)
        {
            var raceSeriesType = raceSeriesTypeGrouping.Key;
            var goal = goals.SingleOrDefault(oo => oo.RaceSeriesType == raceSeriesType);

            return new AthleteCompareStatDto
            {
                ActualTotal = raceSeriesTypeGrouping.Count(),
                GoalTotal = goal?.TotalEvents,
                RaceSeriesType = raceSeriesType.ToString(),
            };
        }

        var stats = resultsForAthlete
            .Join(courses, oo => oo.CourseId, oo => oo.Id, (_, course) => course)
            .GroupBy(oo => oo.Race.RaceSeries.RaceSeriesType)
            .Select(GetCompareAthletesStat)
            .ToList();

        return new AthleteCompareDto
        {
            Id = athlete.Id,
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.GetGenderFormatted(),
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
            Stats = stats
        };
    }
}
