using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GenerateData;

public class ScoreCoursesOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task Score()
    {
        var allCourses = await scoringDbContext.Courses.AsNoTracking().ToListAsync();
        var allBrackets = await scoringDbContext.Brackets.AsNoTracking().ToListAsync();
        var allIntervals = await scoringDbContext.Intervals.AsNoTracking().ToListAsync();
        var allReads = await scoringDbContext.TagReads.AsNoTracking().ToListAsync();
        var allAthleteCourseBrackets = await scoringDbContext.AtheleteCourseBrackets.AsNoTracking().ToListAsync();

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

            var scorer = new CourseScorer(course, brackets, courseReads, athleteBracketsForCourse, intervals);
            var scoringResult = scorer.GetScoringResult();
            return scoringResult;
        }).ToList();

        var metaResults = allScoringResults.SelectMany(oo => oo.MetadataResults);
        var results = allScoringResults.SelectMany(oo => oo.Results);

        await scoringDbContext.BracketMetadataEntries.AddRangeAsync(metaResults);
        await scoringDbContext.Results.AddRangeAsync(results);
        await scoringDbContext.SaveChangesAsync();
    }
}
