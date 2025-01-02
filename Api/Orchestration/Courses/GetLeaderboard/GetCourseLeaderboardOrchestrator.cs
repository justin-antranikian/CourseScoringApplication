using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Courses.GetLeaderboard;

public class GetCourseLeaderboardOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<CourseLeaderboard>> Get(int courseId, int? bracketId, int? intervalId, int startingRank = 1, int take = 50)
    {
        var course = await dbContext.Courses.Include(oo => oo.Intervals).Include(oo => oo.Brackets).SingleAsync(oo => oo.Id == courseId);
        var bracket = bracketId.HasValue ? course.Brackets.Single(oo => oo.Id == bracketId) : course.Brackets.Single(oo => oo.BracketType == BracketType.Overall);

        var results = await GetResults(bracket.Id, intervalId, startingRank, take);
        var metadataEntries = await GetMetadataEntries(bracket.Id, intervalId);
        return GetCourseLeaderboards(results, metadataEntries, course, bracket.Id).ToList();
    }

    private async Task<List<Result>> GetResults(int bracketId, int? intervalId, int startingRank, int take)
    {
        var endingRank = startingRank + (take - 1);

        var query = dbContext.Results
                        .Include(oo => oo.AthleteCourse)
                        .ThenInclude(oo => oo.Athlete)
                        .Where(oo => oo.BracketId == bracketId && oo.Rank >= startingRank && oo.Rank <= endingRank);

        query = intervalId.HasValue ? query.Where(oo => oo.IntervalId == intervalId) : query.Where(oo => oo.IsHighestIntervalCompleted);
        return await query.OrderBy(oo => oo.Rank).ToListAsync();
    }

    private async Task<List<BracketMetadata>> GetMetadataEntries(int bracketId, int? intervalId)
    {
        var query = dbContext.BracketMetadataEntries.Where(oo => oo.BracketId == bracketId);
        query = intervalId.HasValue ? query.Where(oo => oo.IntervalId == intervalId) : query.Where(oo => oo.IntervalId != null);
        return await query.ToListAsync();
    }

    private static IEnumerable<CourseLeaderboard> GetCourseLeaderboards(List<Result> results, List<BracketMetadata> metadata, Course course, int bracketId)
    {
        foreach (var resultsByInterval in results.GroupBy(oo => oo.IntervalId))
        {
            var interval = course.Intervals.Single(oo => oo.Id == resultsByInterval.Key);
            var bracketMeta = metadata.Single(oo => oo.BracketId == bracketId && oo.IntervalId == resultsByInterval.Key);
            var leaderboardResults = resultsByInterval.Select(oo => MapToLeaderboardResult(oo, course, interval)).ToList();

            yield return new CourseLeaderboard
            {
                IntervalName = interval.Name,
                IntervalType = interval.IntervalType,
                Results = leaderboardResults,
                TotalRacers = bracketMeta.TotalRacers
            };
        }
    }

    private static LeaderboardResultDto MapToLeaderboardResult(Result result, Course course, Interval interval)
    {
        var timeOnCoursePace = course.GetPaceWithTime(result.TimeOnCourse, interval.DistanceFromStart);
        var athlete = result.AthleteCourse.Athlete;
        return LeaderboardResultDtoMapper.GetLeaderboardResultDto(result, athlete, timeOnCoursePace, course);
    }
}
