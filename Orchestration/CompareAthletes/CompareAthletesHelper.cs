namespace Orchestration.CompareAthletes;

public class CompareAthletesHelper
{
    private readonly List<Course> _courses;

    private readonly List<Athlete> _athletes;

    private readonly List<Result> _results;

    public CompareAthletesHelper(List<Course> courses, List<Athlete> athletes, List<Result> results)
    {
        _courses = courses;
        _athletes = athletes;
        _results = results;
    }

    public List<CompareAthletesAthleteInfoDto> GetMappedResults() => GetMappedResultsAsIEnumerable().ToList();

    private IEnumerable<CompareAthletesAthleteInfoDto> GetMappedResultsAsIEnumerable()
    {
        foreach (var athlete in _athletes)
        {
            var resultsForAthlete = _results.Where(result => result.AthleteCourse.AthleteId == athlete.Id);
            var mappedResults = resultsForAthlete.Select(MapToCompareAthletesResult).ToList();
            yield return CompareAthletesAthleteInfoDtoMapper.GetCompareAthletesAthleteInfoDto(athlete, _courses, mappedResults);
        }
    }

    private CompareAthletesResult MapToCompareAthletesResult(Result result)
    {
        var course = _courses.Single(oo => oo.Id == result.CourseId);
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
