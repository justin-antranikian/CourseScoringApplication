using Api.DataModels;
using Api.DataModels.Enums;

namespace Api.Orchestration.GetIrp;

public class GetIrpOrchestrator
{
    private readonly GetIrpRepository _getIrpRepository;

    public GetIrpOrchestrator(ScoringDbContext scoringDbContext)
    {
        _getIrpRepository = new GetIrpRepository(scoringDbContext);
    }

    public async Task<IrpDto> GetIrpDto(int athleteCourseId)
    {
        var (results, course, metadataEntries, athleteCourse) = await _getIrpRepository.GetQueryResult(athleteCourseId);

        var intervalResults = GetIrpResultByIntervals(course, metadataEntries, results, athleteCourse).ToList();
        var bracketResults = GetIrpResultByBrackets(course, metadataEntries, results).ToList();

        var highestIntervalResult = results.First(oo => oo.IsHighestIntervalCompleted);
        var highestInterval = course.Intervals.Single(oo => oo.Id == highestIntervalResult.IntervalId);
        var paceWithTimeCumulative = course.GetPaceWithTime(highestIntervalResult.TimeOnCourse, highestInterval.DistanceFromStart);

        var irpDto = IrpDtoMapper.GetIrpDto(athleteCourse, course, paceWithTimeCumulative, bracketResults, intervalResults);
        return irpDto;
    }

    private static IEnumerable<IrpResultByBracketDto> GetIrpResultByBrackets(Course course, List<BracketMetadata> metadataEntries, List<Result> results)
    {
        foreach (var result in results.Where(oo => oo.IsHighestIntervalCompleted).OrderBy(oo => oo.Bracket.BracketType))
        {
            var totalRacers = metadataEntries.Single(oo => oo.BracketId == result.BracketId).TotalRacers;
            var bracket = course.Brackets.Single(oo => oo.Id == result.BracketId);
            yield return IrpResultByBracketDtoMapper.GetIrpResultByBracketDto(bracket, result, totalRacers);
        }
    }

    private static IEnumerable<IrpResultByIntervalDto> GetIrpResultByIntervals(Course course, List<BracketMetadata> metadataEntries, List<Result> results, AthleteCourse athleteCourse)
    {
        var bracketsForAthlete = GetBracketsForAthlete(course, athleteCourse);
        var primaryDivision = bracketsForAthlete.Single(oo => oo.BracketType == BracketType.PrimaryDivision);

        var metadataHelper = new MetadataGetTotalHelper(metadataEntries, bracketsForAthlete);
        var resultsForIntervals = FilterResultsForIntervals(results, primaryDivision.Id);

        IrpResultByIntervalDto previousInterval = null;
        foreach (var interval in course.Intervals.OrderBy(oo => oo.Order))
        {
            var primaryDivisionResult = resultsForIntervals.SingleOrDefault(oo => oo.IntervalId == interval.Id);
            var result = IrpResultByIntervalDtoMapper.GetIrpResultByIntervalDto(course, interval, primaryDivisionResult, previousInterval, metadataHelper);
            yield return result;
            previousInterval = result;
        }
    }

    private static List<Result> FilterResultsForIntervals(List<Result> results, int primaryDivisionId)
    {
        return results.Where(oo => !oo.IsHighestIntervalCompleted && oo.BracketId == primaryDivisionId).ToList();
    }

    private static List<Bracket> GetBracketsForAthlete(Course course, AthleteCourse athleteCourse)
    {
        return course.Brackets.FilterBrackets(athleteCourse.AthleteCourseBrackets);
    }
}
