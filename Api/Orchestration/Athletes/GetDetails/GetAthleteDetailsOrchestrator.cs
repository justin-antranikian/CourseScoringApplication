using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public class GetAthleteDetailsOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<ArpDto> GetArpDto(int athleteId)
    {
        var getArpRepository = new GetArpRepository(scoringDbContext);
        var (results, courses, metadataEntries, athlete) = await getArpRepository.GetQueryResults(athleteId);
        var arpResults = GetResults(results, courses, metadataEntries).ToList();
        return ArpDtoMapper.GetArpDto(athlete, arpResults);
    }

    /// <summary>
    /// Only need the primary division results when showing the list of races.
    /// </summary>
    /// <param name="results"></param>
    /// <param name="courses"></param>
    /// <param name="metadataEntries"></param>
    /// <returns></returns>
    private static IEnumerable<ArpResultDto> GetResults(List<Result> results, List<Course> courses, List<BracketMetadata> metadataEntries)
    {
        var primaryBracketResults = results.Where(oo => oo.Bracket.BracketType == BracketType.PrimaryDivision).ToList();

        foreach (var course in courses.OrderByDescending(oo => oo.StartDate))
        {
            var result = primaryBracketResults.Single(oo => oo.CourseId == course.Id);
            var metadataEntriesForCourse = metadataEntries.Where(oo => oo.CourseId == course.Id).ToList();

            var distanceCompleted = course.Intervals.Single(oo => oo.Id == result.IntervalId).DistanceFromStart;
            var paceWithTimeCumulative = course.GetPaceWithTime(result.TimeOnCourse, distanceCompleted);
            var bracketsForAthlete = course.Brackets.FilterBrackets(metadataEntriesForCourse);
            var metadataHelper = new MetadataGetTotalHelper(metadataEntriesForCourse, bracketsForAthlete);

            yield return ArpResultDtoMapper.GetArpResultDto(result, course, paceWithTimeCumulative, metadataHelper);
        }
    }
}
