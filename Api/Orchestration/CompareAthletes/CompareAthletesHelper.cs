using Api.DataModels;

namespace Api.Orchestration.CompareAthletes;

public class CompareAthletesHelper(List<Course> courses, List<Athlete> athletes, List<Result> results, List<AthleteRaceSeriesGoal> goals)
{
    public List<CompareAthletesAthleteInfoDto> GetMappedResults() => GetMappedResultsAsIEnumerable().ToList();

    private IEnumerable<CompareAthletesAthleteInfoDto> GetMappedResultsAsIEnumerable()
    {
        foreach (var athlete in athletes)
        {
            var resultsForAthlete = results.Where(result => result.AthleteCourse.AthleteId == athlete.Id);
            var mappedResults = resultsForAthlete.Select(MapToCompareAthletesResult).ToList();
            var athleteGoals = goals.Where(oo => oo.AthleteId == athlete.Id).ToList();
            yield return CompareAthletesAthleteInfoDtoMapper.GetCompareAthletesAthleteInfoDto(athlete, courses, mappedResults, athleteGoals);
        }
    }

    private CompareAthletesResult MapToCompareAthletesResult(Result result)
    {
        var course = courses.Single(oo => oo.Id == result.CourseId);
        var paceWithTime = course.GetPaceWithTime(result.TimeOnCourse);

        return new CompareAthletesResult
        (
            result.AthleteCourseId,
            course.RaceId,
            course.Race.Name,
            course.Id,
            course.Name,
            paceWithTime,
            result.OverallRank,
            result.GenderRank,
            result.DivisionRank
        );
    }
}
