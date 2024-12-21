using Api.DataModels;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.CompareIrps;

public class CompareIrpsOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<CompareIrpsAthleteInfoDto>> GetCompareIrpsDto(List<int> athleteCourseIds)
    {
        var filteredAthleteCourseIds = athleteCourseIds.Take(4).ToList();

        var courseId = (await scoringDbContext.AthleteCourses.SingleAsync(oo => oo.Id == filteredAthleteCourseIds.First())).CourseId;
        var course = await scoringDbContext.Courses.Include(oo => oo.Intervals).SingleAsync(oo => oo.Id == courseId);
        var primaryBracket = await scoringDbContext.Brackets.SingleAsync(oo => oo.CourseId == courseId && oo.BracketType == BracketType.Overall);
        var athleteCourses = await GetAthleteCourses(filteredAthleteCourseIds);

        return GetCompareIrpsAthleteInfoDtos(athleteCourses, primaryBracket.Id, course);
    }

    private static List<CompareIrpsAthleteInfoDto> GetCompareIrpsAthleteInfoDtos(List<AthleteCourse> athleteCourses, int primaryBracketId, Course course)
    {
        var orderedAthleteCourses = athleteCourses
                                        .Select(oo => oo.Results.Single(oo => oo.BracketId == primaryBracketId && oo.IsHighestIntervalCompleted))
                                        .OrderBy(oo => oo.Rank)
                                        .Select(oo => oo.AthleteCourse)
                                        .ToList();

        var athleteInfoDtos = orderedAthleteCourses.Select((oo, index) => MapToCompareIrpsAthleteInfoDto(oo, index, course, primaryBracketId)).ToList();
        return athleteInfoDtos;
    }

    private static CompareIrpsAthleteInfoDto MapToCompareIrpsAthleteInfoDto(AthleteCourse athleteCourse, int index, Course course, int primaryBracketId)
    {
        var athlete = athleteCourse.Athlete;
        var filteredResults = athleteCourse.Results.Where(result => result.BracketId == primaryBracketId && !result.IsHighestIntervalCompleted).ToList();

        IEnumerable<CompareIrpsIntervalDto> GetIntervalDtos()
        {
            foreach (var interval in course.Intervals.OrderBy(oo => oo.Order))
            {
                var resultForInterval = filteredResults.SingleOrDefault(result => result.IntervalId == interval.Id);
                var intervalResult = GetIntervalResult(resultForInterval, interval, course);
                yield return intervalResult;
            }
        }

        var intervalDtos = GetIntervalDtos().ToList();
        var fullCourseInterval = course.Intervals.Single(oo => oo.IsFullCourse);
        var fullCourseResult = filteredResults.SingleOrDefault(oo => oo.IntervalId == fullCourseInterval.Id);
        var finishInfo = GetFinishInfo(fullCourseResult, course);
        var irpRank = index.MapToCompareIrpsRank();

        return new CompareIrpsAthleteInfoDto
        (
            athlete.Id,
            athlete.FullName,
            athlete.DateOfBirth,
            athlete.Gender,
            athleteCourse.Bib,
            course.StartDate,
            irpRank,
            athlete.City,
            athlete.State,
            athleteCourse.Id,
            finishInfo,
            intervalDtos
        );
    }

    private static CompareIrpsIntervalDto GetIntervalResult(Result result, Interval interval, Course course)
    {
        if (result == null)
        {
            return new CompareIrpsIntervalDto(interval.Name, null, null, null, null, null);
        }

        var paceWithTime = course.GetPaceWithTime(result.TimeOnCourse, interval.DistanceFromStart);
        var crossingTime = course.GetCrossingTime(result.TimeOnCourse);
        return new CompareIrpsIntervalDto(interval.Name, paceWithTime, crossingTime, result.OverallRank, result.GenderRank, result.DivisionRank);
    }

    private static FinishInfo? GetFinishInfo(Result? fullCourseResult, Course course)
    {
        if (fullCourseResult is null)
        {
            return null;
        }

        var paceWithTime = course.GetPaceWithTime(fullCourseResult.TimeOnCourse);
        var crossingTime = course.GetCrossingTime(fullCourseResult.TimeOnCourse);
        return new FinishInfo(crossingTime, paceWithTime);
    }

    private async Task<List<AthleteCourse>> GetAthleteCourses(List<int> athleteCourseIds)
    {
        var query = scoringDbContext.AthleteCourses
                        .Include(oo => oo.Results)
                        .Include(oo => oo.Athlete)
                        .Where(oo => athleteCourseIds.Contains(oo.Id));

        return await query.ToListAsync();
    }
}
