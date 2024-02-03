using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.ScoreCourses;

public class ScoreCoursesOrchestrator
{
    private readonly ScoringDbContext _scoringDbContext;

    public ScoreCoursesOrchestrator(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task Score()
    {
        var allCourses = await _scoringDbContext.Courses.AsNoTracking().ToListAsync();
        var allBrackets = await _scoringDbContext.Brackets.AsNoTracking().ToListAsync();
        var allIntervals = await _scoringDbContext.Intervals.AsNoTracking().ToListAsync();
        var allReads = await _scoringDbContext.TagReads.AsNoTracking().ToListAsync();
        var allAthleteCourseBrackets = await _scoringDbContext.AtheleteCourseBrackets.AsNoTracking().ToListAsync();

        var statistics = new List<CourseStatistic>();

        var allScoringResults = allCourses.Select(course =>
        {
            var courseId = course.Id;

            var brackets = allBrackets.Where(oo => oo.CourseId == courseId).ToList();
            var intervals = allIntervals.Where(oo => oo.CourseId == courseId).ToList();
            var courseReads = allReads.Where(oo => oo.CourseId == courseId).ToList();
            var athleteBracketsForCourse = allAthleteCourseBrackets.Where(oo => oo.CourseId == courseId).ToList();

            if (courseReads.Count == 0)
            {
                throw new Exception("Cannot Score a race with no tag reads");
            }

            foreach (var bracket in brackets)
            {
                var athleteBracketsForBracket = athleteBracketsForCourse.Where(oo => oo.BracketId == bracket.Id).Select(oo => oo.AthleteCourseId).ToList();
                var readsForBracket = courseReads.Where(oo => athleteBracketsForBracket.Contains(oo.AthleteCourseId)).ToList();

                if (!readsForBracket.Any())
                {
                    continue;
                }

                var maxIntervalReadsForBracket = GetReadsForMaxInterval(readsForBracket, intervals);

                var bracketStatistic = new CourseStatistic
                {
                    CourseId = courseId,
                    BracketId = bracket.Id,
                    AverageTotalTimeInMilleseconds = (int)maxIntervalReadsForBracket.Average(oo => oo.TimeOnCourse),
                    FastestTimeInMilleseconds = maxIntervalReadsForBracket.Min(oo => oo.TimeOnCourse),
                    SlowestTimeInMilleseconds = maxIntervalReadsForBracket.Max(oo => oo.TimeOnCourse),
                };

                statistics.Add(bracketStatistic);
            }

            var maxIntervalReads = GetReadsForMaxInterval(courseReads, intervals);

            var allBracketsStatistic = new CourseStatistic
            {
                CourseId = courseId,
                AverageTotalTimeInMilleseconds = (int)maxIntervalReads.Average(oo => oo.TimeOnCourse),
                FastestTimeInMilleseconds = maxIntervalReads.Min(oo => oo.TimeOnCourse),
                SlowestTimeInMilleseconds = maxIntervalReads.Max(oo => oo.TimeOnCourse),
            };

            statistics.Add(allBracketsStatistic);

            var scorer = new CourseScorer(course, brackets, courseReads, athleteBracketsForCourse, intervals);
            var scoringResult = scorer.GetScoringResult();
            return scoringResult;
        }).ToList();

        var metaResults = allScoringResults.SelectMany(oo => oo.MetadataResults);
        var results = allScoringResults.SelectMany(oo => oo.Results);

        await _scoringDbContext.BracketMetadataEntries.AddRangeAsync(metaResults);
        await _scoringDbContext.Results.AddRangeAsync(results);
        await _scoringDbContext.CourseStatistics.AddRangeAsync(statistics);
        await _scoringDbContext.SaveChangesAsync();

        var courseTypeStats = await GetCourseTypeStatistics();
        await _scoringDbContext.CourseTypeStatistics.AddRangeAsync(courseTypeStats);
        await _scoringDbContext.SaveChangesAsync();
    }

    private async Task<List<CourseTypeStatistic>> GetCourseTypeStatistics()
    {
        var courses = await _scoringDbContext.Courses.ToListAsync();
        var results = await _scoringDbContext.Results.Include(oo => oo.AthleteCourse).Where(oo => oo.IsHighestIntervalCompleted).ToListAsync();
        var resultsGroupedByAthletes = results.GroupBy(oo => oo.AthleteCourse.AthleteId).ToList();
        var stats = new List<CourseTypeStatistic>();

        foreach (var result in resultsGroupedByAthletes)
        {
            var uniqueCourseResults = result.GroupBy(oo => oo.AthleteCourseId).Select(oo => oo.First()).ToList();

            var query = from course in courses
                        join courseResults in uniqueCourseResults on course.Id equals courseResults.CourseId
                        group courseResults by course.CourseType into newGroup
                        select newGroup;

            foreach (var courseTypeGrouping in query.ToList())
            {
                var stat = new CourseTypeStatistic
                {
                    CourseType = courseTypeGrouping.Key,
                    AthleteId = result.Key,
                    AverageTotalTimeInMilleseconds = (int)courseTypeGrouping.Average(oo => oo.TimeOnCourse),
                    FastestTimeInMilleseconds = courseTypeGrouping.Min(oo => oo.TimeOnCourse),
                    SlowestTimeInMilleseconds = courseTypeGrouping.Max(oo => oo.TimeOnCourse),
                };

                stats.Add(stat);
            }
        }

        return stats;
    }

    private static List<TagRead> GetReadsForMaxInterval(List<TagRead> reads, List<Interval> intervals)
    {
        var intervalsFromReads = reads.Select(oo => oo.IntervalId).Distinct().ToList();
        var maxIntervalId = intervals.Where(oo => intervalsFromReads.Contains(oo.Id)).OrderByDescending(oo => oo.Order).First().Id;
        return reads.Where(oo => oo.IntervalId == maxIntervalId).ToList();
    }
}
