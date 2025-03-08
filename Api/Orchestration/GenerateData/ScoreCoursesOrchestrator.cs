using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.GenerateData;

public class ScoreCoursesOrchestrator(ScoringDbContext dbContext)
{
    public async Task Score()
    {
        var allCourses = await dbContext.Courses.AsNoTracking().ToListAsync();
        var allBrackets = await dbContext.Brackets.AsNoTracking().ToListAsync();
        var allIntervals = await dbContext.Intervals.AsNoTracking().ToListAsync();
        var allReads = await dbContext.TagReads.AsNoTracking().ToListAsync();
        var allAthleteCourseBrackets = await dbContext.AtheleteCourseBrackets.AsNoTracking().ToListAsync();

        var allScoringResults = allCourses.Select(course =>
        {
            var courseId = course.Id;

            var brackets = allBrackets.Where(oo => oo.CourseId == courseId).ToList();
            var intervals = allIntervals.Where(oo => oo.CourseId == courseId).ToList();
            var courseReads = allReads.Where(oo => oo.CourseId == courseId).ToList();
            var athleteBracketsForCourse = allAthleteCourseBrackets.Where(oo => oo.CourseId == courseId).ToList();

            var scorer = new CourseScorer(course, brackets, courseReads, athleteBracketsForCourse, intervals);
            var scoringResult = scorer.GetScoringResult();
            return scoringResult;
        }).ToList();

        var metaResults = allScoringResults.SelectMany(oo => oo.MetadataResults);
        var results = allScoringResults.SelectMany(oo => oo.Results);

        await dbContext.BracketMetadataEntries.AddRangeAsync(metaResults);
        await dbContext.Results.AddRangeAsync(results);
        await dbContext.SaveChangesAsync();
    }
}
