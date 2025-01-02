using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Races.GetLeaderboard;

public class GetRaceLeaderboardOrchestrator(ScoringDbContext dbContext)
{
    /// <summary>
    /// Returns the top 3 athletes for a course for all the courses in a race. The results are determined from the overall bracket.
    /// </summary>
    public async Task<RaceLeaderboardDto> GetRaceLeaderboardDto(int raceId)
    {
        var race = await dbContext.Races.SingleAsync(oo => oo.Id == raceId);
        var raceSeries = await dbContext.GetRaceSeriesWithLocationInfo().SingleAsync(oo => oo.Id == race.RaceSeriesId);

        var courses = await dbContext.Courses.Include(oo => oo.Brackets).Include(oo => oo.Intervals).Where(oo => oo.RaceId == raceId).ToListAsync();
        var overallBrackets = courses.SelectMany(oo => oo.Brackets).Where(oo => oo.BracketType == BracketType.Overall);
        var overallBracketIds = overallBrackets.Select(oo => oo.Id).ToList();
        var metadataEntries = await dbContext.BracketMetadataEntries.Where(oo => overallBracketIds.Contains(oo.BracketId) && oo.IntervalId != null && oo.TotalRacers > 0).ToListAsync();

        var allIntervals = courses.SelectMany(oo => oo.Intervals).ToList();
        var highestCompletedIntervals = metadataEntries.GroupBy(oo => oo.CourseId).Select(grouping => GetHighestCompletedInterval(allIntervals, grouping)).ToList();
        var resultsForAllCourses = await GetResults(highestCompletedIntervals, overallBracketIds);

        var mapper = new RaceLeaderboardByCourseMapper(highestCompletedIntervals, resultsForAllCourses);
        var leaderboards = mapper.GetResults(courses).OrderBy(oo => oo.SortOrder).ToList();
        return MapToRaceLeaderboard(race, raceSeries, leaderboards);
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
    /// We only care about the highest intervals completed for each course.
    /// </summary>
    private async Task<List<Result>> GetResults(List<Interval> intervals, List<int> overallBracketIds)
    {
        var intervalIds = intervals.Select(oo => oo.Id).ToList();

        return await dbContext.Results
            .Include(oo => oo.AthleteCourse)
            .ThenInclude(oo => oo.Athlete)
            .Where(oo =>
                intervalIds.Contains(oo.IntervalId) &&
                overallBracketIds.Contains(oo.BracketId) &&
                oo.IsHighestIntervalCompleted == false &&
                oo.Rank >= 1 &&
                oo.Rank <= 3
            )
            .OrderBy(oo => oo.Rank)
            .ToListAsync();
    }

    private static RaceLeaderboardDto MapToRaceLeaderboard(Race race, RaceSeries raceSeries, List<RaceLeaderboardByCourseDto> leaderboards)
    {
        return new RaceLeaderboardDto
        {
            LocationInfoWithRank = raceSeries.ToLocationInfoWithRank(),
            RaceKickOffDate = race.KickOffDate.ToShortDateString(),
            RaceName = race.Name,
            RaceSeriesDescription = raceSeries.Description,
            RaceSeriesType = raceSeries.RaceSeriesType.ToString(),
            Leaderboards = leaderboards,
        };
    }
}
