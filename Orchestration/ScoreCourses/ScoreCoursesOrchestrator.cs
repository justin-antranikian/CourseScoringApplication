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
            }

            var maxIntervalReads = GetReadsForMaxInterval(courseReads, intervals);

            var scorer = new CourseScorer(course, brackets, courseReads, athleteBracketsForCourse, intervals);
            var scoringResult = scorer.GetScoringResult();
            return scoringResult;
        }).ToList();

        var metaResults = allScoringResults.SelectMany(oo => oo.MetadataResults);
        var results = allScoringResults.SelectMany(oo => oo.Results);

        await _scoringDbContext.BracketMetadataEntries.AddRangeAsync(metaResults);
        await _scoringDbContext.Results.AddRangeAsync(results);
        await _scoringDbContext.SaveChangesAsync();
    }

    private static List<TagRead> GetReadsForMaxInterval(List<TagRead> reads, List<Interval> intervals)
    {
        var intervalsFromReads = reads.Select(oo => oo.IntervalId).Distinct().ToList();
        var maxIntervalId = intervals.Where(oo => intervalsFromReads.Contains(oo.Id)).OrderByDescending(oo => oo.Order).First().Id;
        return reads.Where(oo => oo.IntervalId == maxIntervalId).ToList();
    }
}
