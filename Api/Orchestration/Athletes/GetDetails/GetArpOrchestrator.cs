using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Athletes.GetDetails;

public class GetArpOrchestrator(ScoringDbContext dbContext)
{
    public async Task<ArpDto> Get(int athleteId)
    {
        var athlete = await dbContext.GetAthletesWithLocationInfo().Include(oo => oo.AthleteWellnessEntries).SingleAsync(oo => oo.Id == athleteId);

        //var athleteLocation = athlete.Location;
        //var intersectingRaceSeries = await dbContext.RaceSeries.Where(oo => athleteLocation.Intersects(oo.Location)).ToListAsync();

        var results = await GetResults(athleteId);
        var courses = await GetCourses(results);
        var metadataEntries = await GetBracketMetadataEntries(results);

        List<string> GetWellnessEntries(params AthleteWellnessType[] wellnessTypes)
        {
            return athlete.AthleteWellnessEntries.Where(oo => wellnessTypes.Contains(oo.AthleteWellnessType)).Select(oo => oo.Description).ToList();
        }

        return new ArpDto
        {
            Age = DateTimeHelper.GetCurrentAge(athlete.DateOfBirth),
            FirstName = athlete.FirstName,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.GetGenderFormatted(),
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
            Results = GetResults(results, courses, metadataEntries).ToList(),
            WellnessGoals = GetWellnessEntries(AthleteWellnessType.Goal),
            WellnessMotivationalList = GetWellnessEntries(AthleteWellnessType.Motivational),
            WellnessTrainingAndDiet = GetWellnessEntries(AthleteWellnessType.Training, AthleteWellnessType.Diet),
        };
    }

    /// <summary>
    /// Only need the primary division results when showing the list of races.
    /// </summary>
    private static IEnumerable<ArpResultDto> GetResults(List<Result> results, List<Course> courses, List<BracketMetadata> metadataEntries)
    {
        var primaryBracketResults = results.Where(oo => oo.Bracket.BracketType == BracketType.PrimaryDivision).ToList();

        foreach (var course in courses.OrderByDescending(oo => oo.StartDate))
        {
            var result = primaryBracketResults.Single(oo => oo.CourseId == course.Id);
            var metadataEntriesForCourse = metadataEntries.Where(oo => oo.CourseId == course.Id).ToList();

            var distanceCompleted = course.Intervals.Single(oo => oo.Id == result.IntervalId).DistanceFromStart;
            var paceWithTimeCumulative = course.GetPaceWithTime(result.TimeOnCourse, distanceCompleted);
            var brackets = course.Brackets.Join(metadataEntries, oo => oo.Id, oo => oo.BracketId, (bracket, _) => bracket).ToList();
            var metadataHelper = new MetadataGetTotalHelper(metadataEntriesForCourse, brackets);

            yield return new ArpResultDto
            {
                AthleteCourseId = result.AthleteCourseId,
                CourseId = course.Id,
                CourseName = course.Name,
                GenderCount = metadataHelper.GetGenderTotal(),
                GenderRank = result.GenderRank,
                OverallCount = metadataHelper.GetOverallTotal(),
                OverallRank = result.OverallRank,
                PaceWithTimeCumulative = paceWithTimeCumulative,
                PrimaryDivisionCount = metadataHelper.GetPrimaryDivisionTotal(),
                PrimaryDivisionRank = result.DivisionRank,
                RaceId = course.RaceId,
                RaceName = course.Race.Name,
            };
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
