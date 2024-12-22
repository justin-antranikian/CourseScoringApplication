using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.GetRaceLeaderboard;

public class GetRaceLeaderboardOrchestrator
{
    private readonly ScoringDbContext _scoringDbContext;

    public GetRaceLeaderboardOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    /// <summary>
    /// Returns the top 3 athletes for a course for all the courses in a race. The results are determined from the overall bracket.
    /// </summary>
    /// <param name="raceId"></param>
    public async Task<RaceLeaderboardDto> GetRaceLeaderboardDto(int raceId)
    {
        var race = await _scoringDbContext.Races.Include(oo => oo.RaceSeries).SingleAsync(oo => oo.Id == raceId);
        var allCourses = await GetCourses(raceId);
        var overallBrackets = allCourses.SelectMany(oo => oo.Brackets).Where(oo => oo.BracketType == BracketType.Overall);
        var overallBracketIds = overallBrackets.Select(oo => oo.Id).ToList();
        var metadataEntries = await _scoringDbContext.BracketMetadataEntries.Where(oo => overallBracketIds.Contains(oo.BracketId) && oo.IntervalId != null && oo.TotalRacers > 0).ToListAsync();

        var allIntervals = allCourses.SelectMany(oo => oo.Intervals).ToList();
        var highestCompletedIntervals = metadataEntries.GroupBy(oo => oo.CourseId).Select(grouping => GetHighestCompletedInterval(allIntervals, grouping)).ToList();
        var resultsForAllCourses = await GetResults(highestCompletedIntervals, overallBracketIds);

        var mapper = new RaceLeaderboardByCourseMapper(highestCompletedIntervals, resultsForAllCourses);
        var leaderboardDtos = mapper.GetResults(allCourses).OrderBy(oo => oo.SortOrder).ToList();
        return RaceLeaderboardDtoMapper.GetRaceLeaderboardDto(race, leaderboardDtos);
    }

    private async Task<List<Course>> GetCourses(int raceId)
    {
        var query = _scoringDbContext.Courses
                        .Include(oo => oo.Brackets)
                        .Include(oo => oo.Intervals)
                        .Where(oo => oo.RaceId == raceId);

        return await query.ToListAsync();
    }

    private static Interval GetHighestCompletedInterval(List<Interval> allIntervals, IGrouping<int, BracketMetadata> grouping)
    {
        return allIntervals
                .Where(oo => oo.CourseId == grouping.Key)
                .Join(grouping, interval => interval.Id, grouping => grouping.IntervalId, (interval, _) => interval)
                .OrderByDescending(oo => oo.Order)
                .First();
    }

    /// <summary>
    /// We only care about the highest intervals completed for each course. Meaning if there is one athlete in the higher interval,
    /// the leaderboard for that course will have one athlete. It would be too much information to display multiple intervals per couse.
    /// </summary>
    /// <param name="intervals"></param>
    /// <param name="overallBracketIds"></param>
    /// <returns></returns>
    private async Task<List<Result>> GetResults(List<Interval> intervals, List<int> overallBracketIds)
    {
        var intervalIds = intervals.Select(oo => oo.Id).ToList();

        var query = _scoringDbContext.Results
                        .Include(oo => oo.AthleteCourse)
                        .ThenInclude(oo => oo.Athlete)
                        .Where(oo => 
                            intervalIds.Contains(oo.IntervalId) &&
                            overallBracketIds.Contains(oo.BracketId) &&
                            oo.IsHighestIntervalCompleted == false &&
                            oo.Rank >= 1 &&
                            oo.Rank <= 3
                        );

        return await query.OrderBy(oo => oo.Rank).ToListAsync();
    }
}
