using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Courses.GetAwards;

public class GetAwardsOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<AwardsDto>> GetPodiumEntries(int courseId)
    {
        var course = await scoringDbContext.Courses.SingleAsync(oo => oo.Id == courseId);
        var brackets = await scoringDbContext.Brackets.Where(oo => oo.CourseId == courseId).ToListAsync();
        var fullCourseInterval = await scoringDbContext.Intervals.SingleAsync(oo => oo.CourseId == courseId && oo.IsFullCourse);
        var resultsForAllBrackets = await GetResults(courseId, fullCourseInterval);
        return GetAwards(resultsForAllBrackets, brackets, course).ToList();
    }

    private async Task<List<Result>> GetResults(int courseId, Interval fullCourseInterval)
    {
        var query = scoringDbContext.Results
                        .Include(oo => oo.AthleteCourse)
                        .ThenInclude(oo => oo!.Athlete)
                        .Where(oo => oo.CourseId == courseId && !oo.IsHighestIntervalCompleted)
                        .Where(oo => oo.IntervalId == fullCourseInterval.Id && oo.Rank <= 9);

        return await query.ToListAsync();
    }

    private IEnumerable<AwardsDto> GetAwards(List<Result> resultsForAllBrackets, List<Bracket> brackets, Course course)
    {
        var overallBracket = brackets.Single(oo => oo.BracketType == BracketType.Overall);
        var overallBracketWinners = FilterWinnersInHigherBrackets(resultsForAllBrackets, overallBracket.Id);

        yield return MapToDto(overallBracket, overallBracketWinners, course);

        var higherBracketWinners = new List<Result>();
        higherBracketWinners.AddRange(overallBracketWinners);
        foreach (var bracket in brackets.Where(oo => oo.BracketType == BracketType.Gender))
        {
            var bracketWinners = FilterWinnersInHigherBrackets(resultsForAllBrackets, bracket.Id, overallBracketWinners);
            yield return MapToDto(bracket, bracketWinners, course);
            higherBracketWinners.AddRange(bracketWinners);
        }

        BracketType[] divisionTypes = [BracketType.PrimaryDivision, BracketType.NonPrimaryDivision];
        foreach (var bracket in brackets.Where(oo => divisionTypes.Contains(oo.BracketType)))
        {
            var bracketWinners = FilterWinnersInHigherBrackets(resultsForAllBrackets, bracket.Id, higherBracketWinners);
            yield return MapToDto(bracket, bracketWinners, course);
        }
    }

    private static AwardsDto MapToDto(Bracket bracket, List<Result> results, Course course)
    {
        AwardWinnerDto MapToAwardWinnerDto(Result result)
        {
            var athlete = result.AthleteCourse!.Athlete!;
            var finishTime = course.GetCrossingTime(result.TimeOnCourse);
            var paceWithTime = course.GetPaceWithTime(result.TimeOnCourse);

            return new AwardWinnerDto
            {
                AthleteId = athlete.Id,
                AthleteCourseId = result.AthleteCourseId,
                FullName = athlete.FullName,
                FinishTime = finishTime,
                PaceWithTime = paceWithTime
            };
        }

        var awardWinners = results.Select(MapToAwardWinnerDto).ToList();

        AwardWinnerDto? GetAwardWinner(int index)
        {
            return awardWinners.Count > index ? awardWinners[index] : null;
        }

        var firstPlace = GetAwardWinner(0);
        var secondPlace = GetAwardWinner(1);
        var thirdPlace = GetAwardWinner(2);

        return new AwardsDto
        {
            BracketName = bracket.Name,
            FirstPlaceAthlete = firstPlace,
            SecondPlaceAthlete = secondPlace,
            ThirdPlaceAthlete = thirdPlace
        };
    }

    private static List<Result> FilterWinnersInHigherBrackets(List<Result> resultsPool, int bracketId, List<Result>? previousWinners = null)
    {
        IEnumerable<Result> GetWinners(int[] idsThatHaveAlreadyWon)
        {
            return resultsPool
                    .Where(oo => oo.BracketId == bracketId && !idsThatHaveAlreadyWon.Contains(oo.AthleteCourseId))
                    .OrderBy(oo => oo.Rank)
                    .Take(3);
        }

        var idsThatHaveAlreadyWon = (previousWinners ?? []).Select(oo => oo.AthleteCourseId).ToArray();
        return GetWinners(idsThatHaveAlreadyWon).ToList();
    }
}
