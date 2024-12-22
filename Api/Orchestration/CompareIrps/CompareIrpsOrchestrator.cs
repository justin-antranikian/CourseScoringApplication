using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.CompareIrps;

public class CompareIrpsOrchestrator(ScoringDbContext scoringDbContext)
{
    public async Task<List<CompareIrpsAthleteInfoDto>> GetCompareIrpsDto(List<int> athleteCourseIds)
    {
        var courseId = (await scoringDbContext.AthleteCourses.SingleAsync(oo => oo.Id == athleteCourseIds.First())).CourseId;
        var course = await scoringDbContext.Courses.Include(oo => oo.Intervals).SingleAsync(oo => oo.Id == courseId);
        var primaryBracket = await scoringDbContext.Brackets.SingleAsync(oo => oo.CourseId == courseId && oo.BracketType == BracketType.Overall);
        var athleteCourses = await GetAthleteCourses(athleteCourseIds);

        return GetCompareIrpsAthleteInfoDtos(athleteCourses, primaryBracket.Id, course);
    }

    private static List<CompareIrpsAthleteInfoDto> GetCompareIrpsAthleteInfoDtos(List<AthleteCourse> athleteCourses, int primaryBracketId, Course course)
    {
        var orderedAthleteCourses = athleteCourses
                                        .Select(oo => oo.Results.Single(oo => oo.BracketId == primaryBracketId && oo.IsHighestIntervalCompleted))
                                        .OrderBy(oo => oo.Rank)
                                        .Select(oo => oo.AthleteCourse)
                                        .ToList();

        var athleteInfoDtos = orderedAthleteCourses.Select(oo => MapToCompareIrpsAthleteInfoDto(oo, course, primaryBracketId)).ToList();
        return athleteInfoDtos;
    }

    private static CompareIrpsAthleteInfoDto MapToCompareIrpsAthleteInfoDto(AthleteCourse athleteCourse, Course course, int primaryBracketId)
    {
        var athlete = athleteCourse.Athlete;
        var filteredResults = athleteCourse.Results.Where(result => result.BracketId == primaryBracketId && !result.IsHighestIntervalCompleted).ToList();

        IEnumerable<CompareIrpsIntervalDto> GetIntervals()
        {
            foreach (var interval in course.Intervals.OrderBy(oo => oo.Order))
            {
                var resultForInterval = filteredResults.SingleOrDefault(result => result.IntervalId == interval.Id);
                var intervalResult = GetIntervalResult(resultForInterval, interval, course);
                yield return intervalResult;
            }
        }

        return new CompareIrpsAthleteInfoDto
        {
            AthleteCourseId = athleteCourse.Id,
            City = athlete.City,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.Gender.ToAbbreviation(),
            RaceAge = DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
            State = athlete.State,
            CompareIrpsIntervalDtos = GetIntervals().ToList()
        };
    }

    private static CompareIrpsIntervalDto GetIntervalResult(Result? result, Interval interval, Course course)
    {
        if (result == null)
        {
            return new CompareIrpsIntervalDto(interval.Name, null, null, null, null, null);
        }

        var paceWithTime = course.GetPaceWithTime(result.TimeOnCourse, interval.DistanceFromStart);
        var crossingTime = course.GetCrossingTime(result.TimeOnCourse);
        return new CompareIrpsIntervalDto(interval.Name, paceWithTime, crossingTime, result.OverallRank, result.GenderRank, result.DivisionRank);
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
