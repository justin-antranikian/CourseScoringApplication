﻿using Api.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Orchestration.Results.Compare;

public class CompareIrpsOrchestrator(ScoringDbContext dbContext)
{
    public async Task<List<ResultCompareAthleteInfoDto>> Get(List<int> athleteCourseIds)
    {
        var athleteCourses = await dbContext.AthleteCourses.Where(oo => athleteCourseIds.Contains(oo.Id)).ToListAsync();
        var courseId = athleteCourses.First().CourseId;

        var course = await dbContext.Courses.Include(oo => oo.Intervals).SingleAsync(oo => oo.Id == courseId);
        var primaryBracket = await dbContext.Brackets.SingleAsync(oo => oo.CourseId == courseId && oo.BracketType == BracketType.Overall);

        var athleteIds = athleteCourses.Select(oo => oo.AthleteId).ToList();
        var athletes = await dbContext.GetAthletesWithLocationInfo().Where(oo => athleteIds.Contains(oo.Id)).ToListAsync();

        var results = await dbContext.Results.AsNoTracking()
            .Where(oo => athleteCourseIds.Contains(oo.AthleteCourseId))
            .Where(oo => oo.BracketId == primaryBracket.Id)
            .ToListAsync();

        return MapToAthleteInfo(athleteCourses, athletes, results, course).ToList();
    }

    private static IEnumerable<ResultCompareAthleteInfoDto> MapToAthleteInfo(List<AthleteCourse> athleteCourses, List<Athlete> athletes, List<Result> results, Course course)
    {
        foreach (var athleteCourseId in results.Where(oo => oo.IsHighestIntervalCompleted).OrderBy(oo => oo.Rank).Select(oo => oo.AthleteCourseId))
        {
            var athleteCourse = athleteCourses.Single(oo => oo.Id == athleteCourseId);
            var athlete = athletes.Single(oo => oo.Id == athleteCourse.AthleteId);
            var filteredResults = results.Where(oo => oo.AthleteCourseId == athleteCourseId && !oo.IsHighestIntervalCompleted).ToList();
            yield return MapToDto(athleteCourse, athlete, filteredResults, course);
        }
    }

    private static ResultCompareAthleteInfoDto MapToDto(AthleteCourse athleteCourse, Athlete athlete, List<Result> results, Course course)
    {
        IEnumerable<ResultCompareIntervalDto> GetIntervals()
        {
            foreach (var interval in course.Intervals.OrderBy(oo => oo.Order))
            {
                var resultForInterval = results.SingleOrDefault(result => result.IntervalId == interval.Id);
                var intervalResult = GetIntervalResult(resultForInterval, interval, course);
                yield return intervalResult;
            }
        }

        return new ResultCompareAthleteInfoDto
        {
            AthleteCourseId = athleteCourse.Id,
            CourseId = athleteCourse.CourseId,
            FullName = athlete.FullName,
            GenderAbbreviated = athlete.GetGenderFormatted(),
            LocationInfoWithRank = athlete.ToLocationInfoWithRank(),
            RaceAge = DateTimeHelper.GetRaceAge(athlete.DateOfBirth, course.StartDate),
            Intervals = GetIntervals().ToList()
        };
    }

    private static ResultCompareIntervalDto GetIntervalResult(Result? result, Interval interval, Course course)
    {
        if (result == null)
        {
            return new ResultCompareIntervalDto
            {
                CrossingTime = null,
                IntervalName = interval.Name,
                PaceWithTime = null,
                OverallRank = null,
                GenderRank = null,
                PrimaryDivisionRank = null
            };
        }

        var paceWithTime = course.GetPaceWithTime(result.TimeOnCourse, interval.DistanceFromStart);
        var crossingTime = course.GetCrossingTime(result.TimeOnCourse);

        return new ResultCompareIntervalDto
        {
            CrossingTime = crossingTime,
            IntervalName = interval.Name,
            PaceWithTime = paceWithTime,
            OverallRank = result.OverallRank,
            GenderRank = result.GenderRank,
            PrimaryDivisionRank = result.DivisionRank
        };
    }
}
