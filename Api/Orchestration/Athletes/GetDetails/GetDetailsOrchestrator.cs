using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.GetDetails;

public class GetDetailsOrchestrator(ScoringDbContext dbContext)
{
    public async Task<ArpDto> Get(int athleteId)
    {
        var athlete = await dbContext.Athletes
            .Include(oo => oo.AthleteWellnessEntries)
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .SingleAsync(oo => oo.Id == athleteId);

        var results = await GetResults(athleteId);
        var courses = await GetCourses(results);
        var metadataEntries = await GetBracketMetadataEntries(results);

        var arpResults = GetResults(results, courses, metadataEntries).ToList();
        return ArpDtoMapper.GetArpDto(athlete, arpResults);
    }

    /// <summary>
    /// Only need the primary division results when showing the list of races.
    /// </summary>
    /// <param name="results"></param>
    /// <param name="courses"></param>
    /// <param name="metadataEntries"></param>
    /// <returns></returns>
    private static IEnumerable<ArpResultDto> GetResults(List<Result> results, List<Course> courses, List<BracketMetadata> metadataEntries)
    {
        var primaryBracketResults = results.Where(oo => oo.Bracket.BracketType == BracketType.PrimaryDivision).ToList();

        foreach (var course in courses.OrderByDescending(oo => oo.StartDate))
        {
            var result = primaryBracketResults.Single(oo => oo.CourseId == course.Id);
            var metadataEntriesForCourse = metadataEntries.Where(oo => oo.CourseId == course.Id).ToList();

            var distanceCompleted = course.Intervals.Single(oo => oo.Id == result.IntervalId).DistanceFromStart;
            var paceWithTimeCumulative = course.GetPaceWithTime(result.TimeOnCourse, distanceCompleted);
            var bracketsForAthlete = course.Brackets.FilterBrackets(metadataEntriesForCourse);
            var metadataHelper = new MetadataGetTotalHelper(metadataEntriesForCourse, bracketsForAthlete);

            yield return ArpResultDtoMapper.GetArpResultDto(result, course, paceWithTimeCumulative, metadataHelper);
        }
    }

    private async Task<List<Result>> GetResults(int athleteId)
    {
        BracketType[] bracketTypes = [BracketType.Overall, BracketType.Gender, BracketType.PrimaryDivision];

        return await dbContext.Results
            .Include(oo => oo.Bracket)
            .Where(oo => bracketTypes.Contains(oo.Bracket.BracketType) && oo.AthleteCourse.AthleteId == athleteId && oo.IsHighestIntervalCompleted)
            .ToListAsync();
    }

    private async Task<List<Course>> GetCourses(List<Result> results)
    {
        var courseIds = results.Select(oo => oo.CourseId).Distinct().ToList();

        return await dbContext.Courses
            .Include(oo => oo.Brackets)
            .Include(oo => oo.Intervals)
            .Include(oo => oo.Race)
            .AsSplitQuery()
            .Where(oo => courseIds.Contains(oo.Id))
            .ToListAsync();
    }

    private async Task<List<BracketMetadata>> GetBracketMetadataEntries(List<Result> results)
    {
        var bracketIds = results.Select(oo => oo.BracketId).ToList();
        return await dbContext.BracketMetadataEntries.Where(oo => bracketIds.Contains(oo.BracketId) && oo.IntervalId == null).ToListAsync();
    }
}
