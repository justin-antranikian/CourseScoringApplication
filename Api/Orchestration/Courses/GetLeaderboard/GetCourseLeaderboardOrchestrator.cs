using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Courses.GetLeaderboard;

public class GetCourseLeaderboardOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<CourseLeaderboardByIntervalDto>> GetCourseLeaderboardDto(int courseId, int? bracketId, int? intervalId, int startingRank = 1, int take = 50)
    {
        var course = await dbContext.Courses.Include(oo => oo.Intervals).Include(oo => oo.Brackets).SingleAsync(oo => oo.Id == courseId);
        var bracketToUse = bracketId.HasValue ? course.Brackets.Single(oo => oo.Id == bracketId) : course.Brackets.Single(oo => oo.BracketType == BracketType.Overall);

        var results = await GetResults(bracketToUse.Id, intervalId, startingRank, take);
        var metadataEntries = await GetMetadataEntries(bracketToUse.Id, intervalId);
        return GetCourseResultByIntervals(results, metadataEntries, course, bracketToUse.Id).ToList();
    }

    private async Task<List<Result>> GetResults(int bracketId, int? intervalId, int startingRank, int take)
    {
        var endingRank = startingRank + (take - 1);
        var wantsHighestIntervalCompleted = !intervalId.HasValue;

        var query = dbContext.Results
                        .Include(oo => oo.AthleteCourse)
                        .ThenInclude(oo => oo.Athlete)
                        .Where(oo =>
                            oo.BracketId == bracketId &&
                            oo.IsHighestIntervalCompleted == wantsHighestIntervalCompleted &&
                            oo.Rank >= startingRank &&
                            oo.Rank <= endingRank
                        );

        if (intervalId.HasValue)
        {
            query = query.Where(oo => oo.IntervalId == intervalId);
        }

        return await query.OrderBy(oo => oo.Rank).ToListAsync();
    }

    private async Task<List<BracketMetadata>> GetMetadataEntries(int bracketId, int? intervalId)
    {
        var query = dbContext.BracketMetadataEntries.Where(oo => oo.BracketId == bracketId);

        if (intervalId != null)
        {
            query = query.Where(oo => oo.IntervalId == intervalId);
        }
        else
        {
            // just get metadata for all intervals. There might be some extra intervals retrieved, but that amount is negligible.
            query = query.Where(oo => oo.IntervalId != null);
        }

        return await query.ToListAsync();
    }

    private static IEnumerable<CourseLeaderboardByIntervalDto> GetCourseResultByIntervals(List<Result> results, List<BracketMetadata> metadata, Course course, int bracketId)
    {
        foreach (var resultsByInterval in results.GroupBy(oo => oo.IntervalId))
        {
            var interval = course.Intervals.Single(oo => oo.Id == resultsByInterval.Key);
            var bracketMeta = metadata.Single(oo => oo.BracketId == bracketId && oo.IntervalId == resultsByInterval.Key);
            var leaderboardResults = resultsByInterval.Select(oo => MapToLeaderboardResult(oo, course, interval)).ToList();

            yield return new CourseLeaderboardByIntervalDto
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
