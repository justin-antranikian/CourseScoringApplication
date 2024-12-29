using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Results.GetDetails;

public class GetIrpRepository(ScoringDbContext scoringDbContext)
{
    public record QueryResult(List<Result> results, Course course, List<BracketMetadata> metadataEntries, AthleteCourse athleteCourse);

    public async Task<QueryResult> GetQueryResult(int athleteCourseId)
    {
        var athleteCourse = await scoringDbContext.AthleteCourses
                                .Include(oo => oo.Athlete).ThenInclude(oo => oo.AthleteRaceSeriesGoals)
                                .Include(oo => oo.Athlete).ThenInclude(oo => oo.AreaLocation)
                                .Include(oo => oo.Athlete).ThenInclude(oo => oo.CityLocation)
                                .Include(oo => oo.Athlete).ThenInclude(oo => oo.StateLocation)
                                .Include(oo => oo.AthleteCourseBrackets)
                                .Include(oo => oo.AthleteCourseTrainings)
                                .SingleAsync(oo => oo.Id == athleteCourseId);

        var course = await scoringDbContext.Courses
                            .Include(oo => oo.Brackets)
                            .Include(oo => oo.Intervals)
                            .Include(oo => oo.Race)
                            .ThenInclude(oo => oo.RaceSeries)
                            .SingleAsync(oo => oo.Id == athleteCourse.CourseId);

        var raceSeriesId = course.Race.RaceSeriesId;

        var raceSeries = await scoringDbContext.RaceSeries
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .SingleAsync(oo => oo.Id == raceSeriesId);

        var bracketIdsForAthlete = athleteCourse.AthleteCourseBrackets.Select(oo => oo.BracketId).ToList();
        var metadataEntries = await scoringDbContext.BracketMetadataEntries.Where(oo => oo.IntervalId == null && bracketIdsForAthlete.Contains(oo.BracketId)).ToListAsync();
        var results = await GetResults(athleteCourse, course, athleteCourseId);

        return new QueryResult(results, course, metadataEntries, athleteCourse);
    }

    private async Task<List<Result>> GetResults(AthleteCourse athleteCourse, Course course, int athleteCourseId)
    {
        var filteredBrackets = course.Brackets.FilterBrackets(athleteCourse.AthleteCourseBrackets);
        var primaryBracket = filteredBrackets.Single(oo => oo.BracketType == BracketType.PrimaryDivision);

        var query = scoringDbContext.Results
                        .Where(oo => oo.AthleteCourseId == athleteCourseId)
                        .Where(oo => oo.BracketId == primaryBracket.Id || oo.IsHighestIntervalCompleted);

        return await query.ToListAsync();
    }
}
