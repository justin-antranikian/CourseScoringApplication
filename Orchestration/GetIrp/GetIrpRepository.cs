﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Orchestration.GetIrp;

public class GetIrpRepository
{
    public record QueryResult(List<Result> results, Course course, List<BracketMetadata> metadataEntries, AthleteCourse athleteCourse);

    private readonly ScoringDbContext _scoringDbContext;

    public GetIrpRepository(ScoringDbContext scoringDbContext)
    {
        _scoringDbContext = scoringDbContext;
    }

    public async Task<QueryResult> GetQueryResult(int athleteCourseId)
    {
        var athleteCourse = await _scoringDbContext.AthleteCourses
                                .Include(oo => oo.Athlete)
                                .ThenInclude(oo => oo.AthleteRaceSeriesGoals)
                                .Include(oo => oo.AthleteCourseBrackets)
                                .Include(oo => oo.AthleteCourseTrainings)
                                .SingleAsync(oo => oo.Id == athleteCourseId);

        var course = await _scoringDbContext.Courses
                            .Include(oo => oo.Brackets)
                            .Include(oo => oo.Intervals)
                            .Include(oo => oo.Race)
                            .ThenInclude(oo => oo.RaceSeries)
                            .SingleAsync(oo => oo.Id == athleteCourse.CourseId);

        var bracketIdsForAthlete = athleteCourse.AthleteCourseBrackets.Select(oo => oo.BracketId).ToList();
        var metadataEntries = await _scoringDbContext.BracketMetadataEntries.Where(oo => oo.IntervalId == null && bracketIdsForAthlete.Contains(oo.BracketId)).ToListAsync();
        var results = await GetResults(athleteCourse, course, athleteCourseId);

        return new QueryResult(results, course, metadataEntries, athleteCourse);
    }

    private async Task<List<Result>> GetResults(AthleteCourse athleteCourse, Course course, int athleteCourseId)
    {
        var filteredBrackets = course.Brackets.FilterBrackets(athleteCourse.AthleteCourseBrackets);
        var primaryBracket = filteredBrackets.Single(oo => oo.BracketType == BracketType.PrimaryDivision);

        var query = _scoringDbContext.Results
                        .Where(oo => oo.AthleteCourseId == athleteCourseId)
                        .Where(oo => oo.BracketId == primaryBracket.Id || oo.IsHighestIntervalCompleted);

        return await query.ToListAsync();
    }
}
