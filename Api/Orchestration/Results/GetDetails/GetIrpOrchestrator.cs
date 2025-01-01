﻿using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Results.GetDetails;

public class GetIrpOrchestrator(ScoringDbContext dbContext)
{
    public async Task<IrpDto> Get(int athleteCourseId)
    {
        var athleteCourse = await dbContext.AthleteCourses.Include(oo => oo.AthleteCourseBrackets).Include(oo => oo.AthleteCourseTrainings).SingleAsync(oo => oo.Id == athleteCourseId);
        var athlete = await GetAthlete(athleteCourse);
        var course = await GetCourse(athleteCourse);

        var bracketIdsForAthlete = athleteCourse.AthleteCourseBrackets.Select(oo => oo.BracketId).ToList();
        var metadataEntries = await dbContext.BracketMetadataEntries.Where(oo => oo.IntervalId == null && bracketIdsForAthlete.Contains(oo.BracketId)).ToListAsync();
        var results = await GetResults(athleteCourse, course, athleteCourseId);

        var intervalResults = GetIrpResultByIntervals(course, metadataEntries, results, athleteCourse).ToList();
        var bracketResults = GetIrpResultByBrackets(course, metadataEntries, results).ToList();

        var highestIntervalResult = results.First(oo => oo.IsHighestIntervalCompleted);
        var highestInterval = course.Intervals.Single(oo => oo.Id == highestIntervalResult.IntervalId);
        var paceWithTimeCumulative = course.GetPaceWithTime(highestIntervalResult.TimeOnCourse, highestInterval.DistanceFromStart);

        return IrpDtoMapper.GetIrpDto(athlete, athleteCourse, course, paceWithTimeCumulative, bracketResults, intervalResults);
    }

    private async Task<Athlete> GetAthlete(AthleteCourse athleteCourse)
    {
        return await dbContext.Athletes
            .Include(oo => oo.AreaLocation)
            .Include(oo => oo.CityLocation)
            .Include(oo => oo.StateLocation)
            .Include(oo => oo.AthleteRaceSeriesGoals)
            .SingleAsync(oo => oo.Id == athleteCourse.AthleteId);
    }

    private async Task<Course> GetCourse(AthleteCourse athleteCourse)
    {
        return await dbContext.Courses
            .Include(oo => oo.Brackets)
            .Include(oo => oo.Intervals)
            .Include(oo => oo.Race)
            .SingleAsync(oo => oo.Id == athleteCourse.CourseId);
    }

    private async Task<List<Result>> GetResults(AthleteCourse athleteCourse, Course course, int athleteCourseId)
    {
        var filteredBrackets = course.Brackets.FilterBrackets(athleteCourse.AthleteCourseBrackets);
        var primaryBracket = filteredBrackets.Single(oo => oo.BracketType == BracketType.PrimaryDivision);

        return await dbContext.Results
            .Where(oo => oo.AthleteCourseId == athleteCourseId)
            .Where(oo => oo.BracketId == primaryBracket.Id || oo.IsHighestIntervalCompleted)
            .ToListAsync();
    }

    private static IEnumerable<IrpResultByBracketDto> GetIrpResultByBrackets(Course course, List<BracketMetadata> metadataEntries, List<Result> results)
    {
        foreach (var result in results.Where(oo => oo.IsHighestIntervalCompleted).OrderBy(oo => oo.Bracket.BracketType))
        {
            var totalRacers = metadataEntries.Single(oo => oo.BracketId == result.BracketId).TotalRacers;
            var bracket = course.Brackets.Single(oo => oo.Id == result.BracketId);

            yield return new IrpResultByBracketDto
            {
                Id = bracket.Id,
                Name = bracket.Name,
                Rank = result.Rank,
                TotalRacers = totalRacers
            };
        }
    }

    private static IEnumerable<IrpResultByIntervalDto> GetIrpResultByIntervals(Course course, List<BracketMetadata> metadataEntries, List<Result> results, AthleteCourse athleteCourse)
    {
        var bracketsForAthlete = GetBracketsForAthlete(course, athleteCourse);
        var primaryDivision = bracketsForAthlete.Single(oo => oo.BracketType == BracketType.PrimaryDivision);

        var metadataHelper = new MetadataGetTotalHelper(metadataEntries, bracketsForAthlete);
        var resultsForIntervals = FilterResultsForIntervals(results, primaryDivision.Id);

        IrpResultByIntervalDto? previousInterval = null;
        foreach (var interval in course.Intervals.OrderBy(oo => oo.Order))
        {
            var primaryDivisionResult = resultsForIntervals.SingleOrDefault(oo => oo.IntervalId == interval.Id);
            var result = IrpResultByIntervalDtoMapper.GetIrpResultByIntervalDto(course, interval, primaryDivisionResult, previousInterval, metadataHelper);
            yield return result;
            previousInterval = result;
        }
    }

    private static List<Result> FilterResultsForIntervals(List<Result> results, int primaryDivisionId)
    {
        return results.Where(oo => !oo.IsHighestIntervalCompleted && oo.BracketId == primaryDivisionId).ToList();
    }

    private static List<Bracket> GetBracketsForAthlete(Course course, AthleteCourse athleteCourse)
    {
        return course.Brackets.FilterBrackets(athleteCourse.AthleteCourseBrackets);
    }
}
