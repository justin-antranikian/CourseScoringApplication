﻿using Api.DataModels;

namespace Api.Orchestration.Races.GetLeaderboard;

public class RaceLeaderboardByCourseMapper(List<Interval> highestCompletedIntervalsForAllCourses, List<Result> resultsForAllCourses)
{
    /// <summary>
    /// Takes in list a list of courses and returns a list of the top 3 athletes for each course.
    /// </summary>
    /// <param name="courses"></param>
    /// <returns></returns>
    public IEnumerable<RaceLeaderboardByCourseDto> GetResults(List<Course> courses)
    {
        foreach (var course in courses)
        {
            var interval = highestCompletedIntervalsForAllCourses.Single(oo => oo.CourseId == course.Id);

            yield return new RaceLeaderboardByCourseDto
            {
                CourseId = course.Id,
                CourseName = course.Name,
                SortOrder = interval.Order,
                Results = GetLeaderboardResults(course, interval).ToList()
            };
        }
    }

    private IEnumerable<LeaderboardResultDto> GetLeaderboardResults(Course course, Interval intervalForCourse)
    {
        foreach (var result in resultsForAllCourses.Where(oo => oo.IntervalId == intervalForCourse.Id))
        {
            var distanceCompleted = intervalForCourse.DistanceFromStart;
            var paceWithTimeCumulative = course.GetPaceWithTime(result.TimeOnCourse, distanceCompleted);
            var athlete = result.AthleteCourse.Athlete;
            yield return LeaderboardResultDtoMapper.GetLeaderboardResultDto(result, athlete, paceWithTimeCumulative, course);
        }
    }
}
