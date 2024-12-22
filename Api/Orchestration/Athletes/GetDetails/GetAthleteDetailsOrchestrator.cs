using Api.DataModels;

namespace Api.Orchestration.Athletes.GetDetails;

public class GetAthleteDetailsOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<ArpDto> GetArpDto(int athleteId)
    {
        var getArpRepository = new GetArpRepository(scoringDbContext);
        var (results, courses, metadataEntries, athlete) = await getArpRepository.GetQueryResults(athleteId);
        var arpResults = GetResults(results, courses, metadataEntries).ToList();
        var raceSeriesTypes = Enum.GetValues<RaceSeriesType>();

        ArpGoalDto MapToGoal(RaceSeriesType seriesType)
        {
            var goal = athlete.AthleteRaceSeriesGoals.FirstOrDefault(oo => oo.RaceSeriesType == seriesType);
            var coursesByType = courses.Where(oo => oo.Race.RaceSeries.RaceSeriesType == seriesType).ToList();
            var totalDistance = coursesByType.Sum(oo => oo.Distance);
            var resultsByType = arpResults.Where(oo => oo.RaceSeriesType == seriesType).ToArray();
            return ArpGoalDtoMapper.GetArpGoalDto(seriesType, goal?.TotalEvents ?? 0, resultsByType.Count(), totalDistance, coursesByType);
        }

        var goals = raceSeriesTypes.Select(MapToGoal).ToList();
        var allEventsGoal = GetAllEventsGoal(goals, arpResults, courses);
        var arpDto = ArpDtoMapper.GetArpDto(athlete, arpResults, goals, allEventsGoal);
        return arpDto;
    }

    private static ArpGoalDto GetAllEventsGoal(List<ArpGoalDto> goals, List<ArpResultDto> arpResults, List<Course> courses)
    {
        var goalTotal = goals.Sum(oo => oo.GoalTotal);
        var actualTotal = arpResults.Count();
        var distance = courses.Sum(oo => oo.Distance);
        return ArpGoalDtoMapper.GetArpGoalDto(null, goalTotal, actualTotal, distance, courses);
    }

    /// <summary>
    /// Only need the primary division results when showing the list of races.
    /// </summary>
    /// <param name="results"></param>
    /// <param name="courses"></param>
    /// <param name="metadataEntries"></param>
    /// <returns></returns>
    private static IEnumerable<ArpResultDto> GetResults(List<ResultWithBracketType> results, List<Course> courses, List<BracketMetadata> metadataEntries)
    {
        var primaryBracketResults = results.Where(oo => oo.BracketType == BracketType.PrimaryDivision).ToList();

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
