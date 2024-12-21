using Api.DataModels;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GetAwardsPodium;

public class GetAwardsPodiumOrchestrator
{
    private readonly ScoringDbContext _scoringDbContext;

    public GetAwardsPodiumOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task<List<PodiumEntryDto>> GetPodiumEntries(int courseId)
    {
        var course = await _scoringDbContext.Courses.SingleAsync(oo => oo.Id == courseId);
        var brackets = await _scoringDbContext.Brackets.Where(oo => oo.CourseId == courseId).ToListAsync();
        var fullCourseInterval = await _scoringDbContext.Intervals.SingleAsync(oo => oo.CourseId == courseId && oo.IsFullCourse);
        var resultsForAllBrackets = await GetResults(courseId, fullCourseInterval);
        var podiumEntries = MapToPodiumEntries(resultsForAllBrackets, brackets, course).ToList();
        return podiumEntries;
    }

    private async Task<List<Result>> GetResults(int courseId, Interval fullCourseInterval)
    {
        var query = _scoringDbContext.Results
                        .Include(oo => oo.AthleteCourse)
                        .ThenInclude(oo => oo.Athlete)
                        .Where(oo => oo.CourseId == courseId && !oo.IsHighestIntervalCompleted)
                        .Where(oo => oo.IntervalId == fullCourseInterval.Id && oo.Rank <= 9);

        return await query.ToListAsync();
    }

    private IEnumerable<PodiumEntryDto> MapToPodiumEntries(List<Result> resultsForAllBrackets, List<Bracket> brackets, Course course)
    {
        var overallBracket = brackets.Single(oo => oo.BracketType == BracketType.Overall);
        var overallBracketWinners = FilterWinnersInHigherBrackets(resultsForAllBrackets, overallBracket.Id);

        yield return MapToPodiumEntry(overallBracket, overallBracketWinners, course);

        var higherBracketWinners = new List<Result>();
        higherBracketWinners.AddRange(overallBracketWinners);
        foreach (var bracket in brackets.Where(oo => oo.BracketType == BracketType.Gender))
        {
            var bracketWinners = FilterWinnersInHigherBrackets(resultsForAllBrackets, bracket.Id, overallBracketWinners);
            yield return MapToPodiumEntry(bracket, bracketWinners, course);
            higherBracketWinners.AddRange(bracketWinners);
        }

        var divisionTypes = new[] { BracketType.PrimaryDivision, BracketType.NonPrimaryDivision };
        foreach (var bracket in brackets.Where(oo => divisionTypes.Contains(oo.BracketType)))
        {
            var bracketWinners = FilterWinnersInHigherBrackets(resultsForAllBrackets, bracket.Id, higherBracketWinners);
            yield return MapToPodiumEntry(bracket, bracketWinners, course);
        }
    }

    private PodiumEntryDto MapToPodiumEntry(Bracket bracket, List<Result> results, Course course)
    {
        AwardWinnerDto MapToAwardWinnerDto(Result result)
        {
            var athlete = result.AthleteCourse.Athlete;
            var finishTime = course.GetCrossingTime(result.TimeOnCourse);
            var paceWithTime = course.GetPaceWithTime(result.TimeOnCourse);
            return new AwardWinnerDto(athlete.Id, result.AthleteCourseId, athlete.FullName, finishTime, paceWithTime);
        }

        var awardWinners = results.Select(MapToAwardWinnerDto).ToList();

        AwardWinnerDto? GetAwardWinner(int index)
        {
            return awardWinners.Count > index ? awardWinners[index] : null;
        }

        var firstPlace = GetAwardWinner(0);
        var secondPlace = GetAwardWinner(1);
        var thirdPlace = GetAwardWinner(2);

        return new PodiumEntryDto(bracket.Name, firstPlace, secondPlace, thirdPlace);
    }

    private List<Result> FilterWinnersInHigherBrackets(List<Result> resultsPool, int bracketId, List<Result>? previousWinners = null)
    {
        IEnumerable<Result> GetWinners(int[] idsThatHaveAlreadyWon)
        {
            return resultsPool
                    .Where(oo => oo.BracketId == bracketId && !idsThatHaveAlreadyWon.Contains(oo.AthleteCourseId))
                    .OrderBy(oo => oo.Rank)
                    .Take(3);
        }

        var idsThatHaveAlreadyWon = (previousWinners ?? new List<Result>()).Select(oo => oo.AthleteCourseId).ToArray();
        var winners = GetWinners(idsThatHaveAlreadyWon).ToList();
        return winners;
    }
}
